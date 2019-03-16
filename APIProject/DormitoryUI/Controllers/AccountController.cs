using BusinessLogic.Define;
using DataAccess.Database;
using IdentityManager.Entities;
using IdentityManager.IdentityService;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace DormitoryUI.Controllers
{
    //[Authorize(Roles = "Administrator")]
    [RoutePrefix("account")]
    public class AccountController : ApiController
    {
        private AccountAdapter _accountAdapter;
        public ICustomerService _customerService;
        public IBrandService _brandService;
        public IEmployeeService _employeeService;

        public AccountController(IEntityContext context, ICustomerService customerService, 
            IBrandService brandService, IEmployeeService employeeService)
        {
            _accountAdapter = new AccountAdapter(context);
            _customerService = customerService;
            _brandService = brandService;
            _employeeService = employeeService;
        }

        
            


        /// <summary>
        ///  Lấy thông tin khách hàng trong token
        /// </summary>
        /// <remarks>
        /// Used for get session CUSTOMER's information
        /// </remarks>
        /// <returns></returns>
        [Authorize, HttpGet, Route("current-customer")]
        public async Task<IHttpActionResult> GetCurrentUser()
        {
            try
            {
                var customerId = await _accountAdapter.GetCustomerByAccount(User.Identity.GetUserId());
                
                var customer = _customerService.Get(_ => _.Id == customerId, x => x.CustomerContracts
                .Select(y => y.Contract.Room.Apartment.Brand));


                var brandId = customer.CustomerContracts.FirstOrDefault().Contract.Room.Apartment.Brand.Id;

                return Ok(new
                {
                    customer.Fullname,
                    customer.Email,
                    customer.Sex,
                    customer.Phone,
                    birthdate = customer.Birthdate.ToString("dd/MM/yyyy"),
                    customer.Id,
                    brandId
                });
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [Authorize, HttpGet, Route("current-employee")]
        public async Task<IHttpActionResult> GetCurrentEmp()
        {
            try
            {

                var emp = await _accountAdapter.GetEmployeeByAccount(User.Identity.GetUserId());
                if (emp == null) BadRequest("Invalid employee token");

                var brandId = emp.BrandId;

                return Ok(new
                {
                    emp.Fullname,
                    emp.Email,
                    emp.Phone,
                    emp.Role,
                    brandId,
                });
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }


        [Authorize, HttpPost, Route("change-password")]
        public async Task<IHttpActionResult> ChangePassword(ChangePassViewModel viewModel)
        {
            try
            {
                return Ok((await _accountAdapter.ChangePassword(User.Identity.GetUserId(),
                    viewModel.CurrentPassword, viewModel.NewPassword)));
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }


        //[Authorize(Roles = nameof(AccountType.ADMINISTRATOR))]
        /// <summary>
        ///  Để tạo account đăng nhập. đầy đủ 3 role nha. 
        /// </summary>
        /// <remarks>
        /// For ADMINISTRATOR to add account, see AccountType :
        ///  ADMINISTRATOR = 0,
        ///  EMPLOYEE = 1,
        ///  CUSTOMER = 2
        /// </remarks>
        /// <param name="models.accountType"></param>
        /// <returns></returns>
        [HttpPost, Route("register")]
        public async Task<IHttpActionResult> Register(RegisterViewModel models)
        {
            try
            {
                IdentityInfor infor = await _accountAdapter.Register(models.Username, models.Password, models.AccountType);

                if (infor.IsError)
                {
                    foreach (var error in infor.Errors)
                    {
                        ModelState.AddModelError(string.Empty, new Exception(error));
                    }
                    return BadRequest(ModelState);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        //[Authorize(Roles = nameof(AccountType.ADMINISTRATOR))]
        //[HttpPost]
        //[Route("api/register-full")]
        //public async Task<IHttpActionResult> RegisterFullInfo(RegisterViewModel models)
        //{
        //    try
        //    {
        //        IdentityInfor infor = await _accountAdapter.Register(models.Username, models.Password, models.AccountType);

        //        if (infor.IsError)
        //        {
        //            foreach (var error in infor.Errors)
        //            {
        //                ModelState.AddModelError(string.Empty, new Exception(error));
        //            }
        //            return BadRequest(ModelState);
        //        }
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}
    }



    public class RegisterViewModel
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public AccountType AccountType { get; set; }

    }

    public class ChangePassViewModel
    {
        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }
    }
}
