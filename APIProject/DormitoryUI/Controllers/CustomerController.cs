using BusinessLogic.Define;
using DataAccess.Database;
using DataAccess.Entities;
using DormitoryUI.ViewModels;
using IdentityManager.Entities;
using IdentityManager.IdentityService;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;

namespace DormitoryUI.Controllers
{
    [RoutePrefix("customer")]
    public class CustomerController : BaseController
    {
        public readonly ICustomerService _customerService;
        public readonly IContractService _contractService;
        public readonly IRoomService _roomService;
        public readonly ICustomerContractService _customerContractService;

        public readonly AccountAdapter _accountService;

        public CustomerController(IEntityContext context, ICustomerService customerService, 
            IContractService contractService, IRoomService roomService, 
            ICustomerContractService customerContractService)
        {
            _accountService = new AccountAdapter(context);
            _customerService = customerService;
            _contractService = contractService;
            _roomService = roomService;
            _customerContractService = customerContractService;
        }

        [HttpPost, Route("")]
        [Authorize]
        public async Task<IHttpActionResult> CreateCustomer(CustomerCreateVM viewModel)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("Invalid model");

                if (!User.IsInRole(AccountType.EMPLOYEE.ToString()))
                {
                    return Unauthorized();
                }
                var emp = await _accountService.GetEmployeeByAccount(User.Identity.GetUserId());

                var username = NonUnicode(viewModel.FirstName);
                Customer customer = ModelMapper.ConvertToModel(viewModel);
                customer.BrandId = emp.BrandId ?? 1;

                _customerService.Create(customer);

                customer.Username = username + customer.Id;
                _customerService.Update(customer);

                if (viewModel.ContractId != null)
                {
                    if (_contractService.Get(_ => _.Id == viewModel.ContractId) == null)
                        return BadRequest("Contract not found");

                    _customerContractService.Create(new CustomerContract
                    {
                        ContractId = viewModel.ContractId ?? 1,
                        CustomerId = customer.Id
                    });
                }


                // Create account
                IdentityInfor infor = await _accountService.RegisterCustomer(customer.Id, username);

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



        [HttpGet, Route("")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var result = _customerService.Get(_ => _.Id == id);
                if (result == null) return BadRequest("Customer not found");
                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        /// <summary>
        /// Lấy những hợp đồng của 1 customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet, Route("customer-contract")]
        public IHttpActionResult GetCustomerContract(int customerId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var  lx = _customerService.Get(_ => _.Id == customerId, _ => _.CustomerContracts,
                    _ => _.CustomerContracts.Select(__ => __.Contract));

                return Ok(lx);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpDelete, Route("")]
        public IHttpActionResult Delete(int customerId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var c = _customerService.Get(_ => _.Id == customerId);
                c.Status = false;
                _customerService.Update(c);
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet, Route("all-brand-customer")]
        public IHttpActionResult GetAll(int brandId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var result = _customerService.GetAll(_ => _.CustomerContracts.Select(__ => __.Contract.Room.Apartment))
                    .Where(_ => _.CustomerContracts.FirstOrDefault().Contract.Room.Apartment.BrandId == brandId).ToList();



                return Ok(ModelMapper.ConvertToViewModel(result));
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet, Route("all-customer")]
        [Authorize]
        public async Task<IHttpActionResult> GetAll()
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                List<Customer> result = null;
                if (User.IsInRole(AccountType.ADMINISTRATOR.ToString()))
                {
                    result = _customerService.GetAll().ToList();
                }
                else
                {
                    var emp = await _accountService.GetEmployeeByAccount(User.Identity.GetUserId());
                    result = _customerService.GetAll(_ => _.CustomerContracts.Select(z => z.Contract
                    .Room.Apartment))
                        .Where(_ => _.BrandId == emp.BrandId && _.Status).ToList();
                }

                return Ok(ModelMapper.ConvertToViewModel(result));
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPut, Route("")]
        public IHttpActionResult Update(CustomerUpdateVM viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var customer = _customerService.Get(_ => _.Id == viewModel.Id);
                if (customer == null) return BadRequest("Customer not found");

                customer.Fullname = viewModel.Fullname;
                customer.Phone = viewModel.Phone;
                customer.Birthdate = viewModel.Birthdate; ;
                customer.Sex = viewModel.Sex;
                customer.Email = viewModel.Email;

                _customerService.Update(customer);

                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

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

}
