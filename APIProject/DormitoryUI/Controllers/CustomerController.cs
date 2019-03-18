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

        public readonly AccountAdapter _accountService;

        public CustomerController(IEntityContext context, ICustomerService customerService, 
            IContractService contractService, IRoomService roomService)
        {
            _accountService = new AccountAdapter(context);
            _customerService = customerService;
            _contractService = contractService;
            _roomService = roomService;
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

        //[HttpPost, Route("")]
        //public IHttpActionResult Create(CustomerCreateVM viewModel)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //            return BadRequest();

        //        _customerService.Create(ModelMapper.ConvertToModel(viewModel));

        //        return Ok();
        //    }
        //    catch (Exception e)
        //    {
        //        return InternalServerError(e);
        //    }
        //}

        [HttpGet, Route("all-brand-customer")]
        public IHttpActionResult GetAll(int brandId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var result = _customerService.GetAll(_ => _.CustomerContracts.Select(__ => __.Contract.Room.Apartment))
                    .Where(_ => _.CustomerContracts.FirstOrDefault().Contract.Room.Apartment.BrandId == brandId);



                return Ok(result);
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

                if (User.IsInRole(AccountType.EMPLOYEE.ToString()))
                    return Unauthorized();

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
                        .Where(_ => _.CustomerContracts.FirstOrDefault()
                        .Contract.Room.Apartment.BrandId == emp.BrandId).ToList();
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

                _customerService.Update(ModelMapper.ConvertToModel(viewModel));

                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
