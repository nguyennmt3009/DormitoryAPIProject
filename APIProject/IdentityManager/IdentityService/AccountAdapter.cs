using BusinessLogic.Define;
using BusinessLogic.Implement;
using DataAccess.Database;
using DataAccess.Entities;
using DataAccess.Repositories;
using DataAccess.Repository.Implement;
using IdentityManager.Database;
using IdentityManager.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Transactions;

namespace IdentityManager.IdentityService
{
    public class AccountAdapter
    {
        public AccountService _accountService { get; set; }
        public RoleService _roleService { get; set; }
        public ICustomerService _customerService { get; set; }
        public IEmployeeService _employeeService { get; set; }
        private IUnitOfWork _unitOfWork;
        public IdentityContext Context;

        public AccountAdapter(IEntityContext context)
        {
            _unitOfWork = new UnitOfWork(context);
            Context = new IdentityContext();
            this._accountService = new AccountService(new UserStore<Account>(Context));
            this._roleService = new RoleService(new RoleStore<Role>(Context));
            this._customerService = new CustomerService(_unitOfWork);
            this._employeeService = new EmployeeService(_unitOfWork);
        }

        public async Task<int> GetCustomerByAccount(string id)
        {
            var customerId = (await _accountService.FindByIdAsync(id)).UserDormitoryId ?? -1;
            return customerId;
        }

        public async Task<Employee> GetEmployeeByAccount(string id)
        {
            var accountId = (await _accountService.FindByIdAsync(id)).UserDormitoryId ?? -1;
            return _employeeService.Get(_ => _.Id == accountId);
        }

        public async Task<IdentityInfor> ChangePassword(string userId, string currentPassword, string newPassword)
        {
            IdentityInfor result = new IdentityInfor();
            try
            {
                var user = await this._accountService.ChangePasswordAsync(userId, currentPassword, newPassword);
                if (!user.Succeeded)
                    result.Errors.AddRange(user.Errors);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }


        public async Task<IdentityInfor> Login(string username, string password, string authenticationType)
        {
            IdentityInfor infor = new IdentityInfor();
            try
            {
                Account account = await _accountService.FindAsync(username, password);

                if (account == null)
                {
                    infor.Errors.Add("Invalid username or password");
                }
                else
                {
                    ClaimsIdentity claimsIdentity = await _accountService.CreateIdentityAsync(account,
                        string.IsNullOrEmpty(authenticationType)
                        ? DefaultAuthenticationTypes.ApplicationCookie
                        : authenticationType);
                    infor.Data = claimsIdentity;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return infor;
        }

        public List<object> GetAllEmployeeAccount()
        {
            Role adminRole = _roleService.FindByName(AccountType.ADMINISTRATOR.ToString());
            Role employeeRole = _roleService.FindByName(AccountType.EMPLOYEE.ToString());

            IEnumerable<Account> admins = _accountService.Users
                .Where(z => z.Roles.Any(y => y.RoleId == adminRole.Id));

            IEnumerable<Account> emps = _accountService.Users
                .Where(z => z.Roles.Any(y => y.RoleId == employeeRole.Id));

            List<object> result = new List<object>();
            admins.ToList().ForEach(_ =>
            {
                var tmp = _employeeService.Get(a => a.Id == _.UserDormitoryId);
                result.Add(new
                {
                    tmp.Id,
                    tmp.Fullname,
                    tmp.Phone,
                    tmp.BrandId,
                    tmp.Email,
                    RoleName = AccountType.ADMINISTRATOR.ToString()
                });
            });

            emps.ToList().ForEach(_ =>
            {
                var tmp = _employeeService.Get(a => a.Id == _.UserDormitoryId);
                result.Add(new
                {
                    tmp.Id,
                    tmp.Fullname,
                    tmp.Phone,
                    tmp.BrandId,
                    tmp.Email,
                    RoleName = AccountType.EMPLOYEE.ToString()
                });
            });

            return result;
        }

        public async Task<IdentityInfor> Register(string username, string password, AccountType roleType)
        {
            IdentityInfor infor = new IdentityInfor();

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required,
                TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    Account account = new Account
                    {
                        UserName = username,
                    };

                    switch(roleType)
                    {
                        case AccountType.ADMINISTRATOR:
                        case AccountType.EMPLOYEE:
                            Employee employee = new Employee() {
                                Username = username,
                                Role = roleType.ToString()
                            };
                            _employeeService.Create(employee);
                            account.UserDormitoryId = employee.Id;
                            break;
                        case AccountType.CUSTOMER:
                            Customer customer = new Customer()
                            {
                                Username = username
                            };
                            _customerService.Create(customer);
                            account.UserDormitoryId = customer.Id;
                            break;
                    }

                    IdentityResult createUser = await _accountService.CreateAsync(account, password);

                    if (!createUser.Succeeded)
                        infor.Errors.AddRange(createUser.Errors);
                    else
                    {
                        string roleName = roleType.ToString();
                        if (!_roleService.RoleExists(roleName))
                        {
                            IdentityResult createRole = await this._roleService.CreateAsync(new Role { Name = roleName });

                            if (!createRole.Succeeded)
                                infor.Errors.AddRange(createRole.Errors);
                        }

                        Account currentUser = await _accountService.FindByNameAsync(username);
                        if (!_accountService.IsInRole(currentUser.Id, roleName))
                        {
                            await _accountService.AddToRoleAsync(currentUser.Id, roleName);
                        }
                        
                    }
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return infor;
        }

    }
}
