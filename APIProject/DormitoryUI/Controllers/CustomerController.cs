using BusinessLogic.Define;
using DormitoryUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DormitoryUI.Controllers
{
    [RoutePrefix("customer")]
    public class CustomerController : BaseController
    {
        public readonly ICustomerService _customerService;
        public readonly IContractService _contractService;
        public readonly IRoomService _roomService;

        public CustomerController(ICustomerService customerService, 
            IContractService contractService, IRoomService roomService)
        {
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

        [HttpPost, Route("")]
        public IHttpActionResult Create(CustomerCreateVM viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                _customerService.Create(ModelMapper.ConvertToModel(viewModel));

                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet, Route("all-customer")]
        public IHttpActionResult GetAll()
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var result = _customerService.GetAll();

                return Ok(result);
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
