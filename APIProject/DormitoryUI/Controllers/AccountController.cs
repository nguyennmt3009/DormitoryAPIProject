using BusinessLogic.Define;
using DataAccess.Database;
using DataAccess.Entities;
using DormitoryUI.ViewModels;
using IdentityManager.Entities;
using IdentityManager.IdentityService;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;

namespace DormitoryUI.Controllers
{
    //[Authorize(Roles = "Administrator")]
    [RoutePrefix("account")]
    public class AccountController : BaseController
    {
        private AccountAdapter _accountAdapter;
        public ICustomerService _customerService;
        public IBrandService _brandService;
        public IEmployeeService _employeeService;
        public ICustomerContractService _customerContractService;
        public IContractService _contractService;

        public AccountController(IEntityContext context, ICustomerService customerService, 
            IBrandService brandService, IEmployeeService employeeService,
            ICustomerContractService customerContractService, IContractService contractService)
        {
            _accountAdapter = new AccountAdapter(context);
            _customerService = customerService;
            _brandService = brandService;
            _employeeService = employeeService;
            _customerContractService = customerContractService;
            _contractService = contractService;
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

        


        [HttpPost, Route("customer")]
        public async Task<IHttpActionResult> CreateCustomer(CustomerCreateVM viewModel)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("Invalid model");

                var contract = _contractService.Get(z => z.Id == viewModel.ContractId);
                if (contract == null) return BadRequest("Contract not found");

                var username = NonUnicode(viewModel.FirstName);
                Customer customer = ModelMapper.ConvertToModel(viewModel);
                _customerService.Create(customer);

                customer.Username = username + customer.Id;
                _customerService.Update(customer);
                _customerContractService.Create(new CustomerContract
                {
                    ContractId = contract.Id,
                    CustomerId = customer.Id
                });

                IdentityInfor infor = await _accountAdapter.RegisterCustomer(customer.Id, username);

                if (infor.IsError)
                {
                    foreach (var error in infor.Errors)
                    {
                        ModelState.AddModelError(string.Empty, new Exception(error));
                    }
                    return BadRequest(ModelState);
                }

                return Ok(ModelMapper.ConvertToViewModel(customer));
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
        
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

        [HttpDelete, Route("customer")]
        public IHttpActionResult deleteCustomer(int customerId)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("Invalid model");

                var customer = _customerService.Get(_ => _.Id == customerId);
                if (customer == null) return BadRequest("Customer not found");

                _customerService.Delete(customer);
                _accountAdapter.DeleteCustomer(customerId);

                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
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

        private string NonUnicode(string text)
        {
            text = Regex.Replace(text, @"\s+", "").ToLower();
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
            "đ",
            "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
            "í","ì","ỉ","ĩ","ị",
            "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
            "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
            "ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
            "d",
            "e","e","e","e","e","e","e","e","e","e","e",
            "i","i","i","i","i",
            "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
            "u","u","u","u","u","u","u","u","u","u","u",
            "y","y","y","y","y",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return text;
        }
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
