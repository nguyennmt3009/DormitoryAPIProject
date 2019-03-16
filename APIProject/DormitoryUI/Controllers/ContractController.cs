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
    [RoutePrefix("contract")]
    public class ContractController : BaseController
    {
        public readonly IContractService _contractService;
        public readonly ICustomerContractService _customerContractService;

        public ContractController(IContractService contractService, ICustomerContractService customerContractService)
        {
            _contractService = contractService;
            _customerContractService = customerContractService;
        }

        [HttpGet, Route("")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var result = _contractService.Get(_ => _.Id == id, _ => _.Room);

                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }



        [HttpPost, Route("")]
        public IHttpActionResult Create(ContractCreateVM viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                _contractService.Create(ModelMapper.ConvertToModel(viewModel));

                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet, Route("all-contract")]
        public IHttpActionResult GetAll()
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var result = _contractService.GetAll(_ => _.Room);

                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPut, Route("")]
        public IHttpActionResult Update(ContractUpdateVM viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                _contractService.Update(ModelMapper.ConvertToModel(viewModel));

                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPost, Route("contract-customer")]
        public IHttpActionResult CreateContractCustomer(ContractCustomerCreateVM viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                _customerContractService.Create(ModelMapper.ConvertToModel(viewModel));

                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet, Route("all-contract-customer")]
        public IHttpActionResult GetAllContractCustomer()
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var result = _customerContractService.GetAll();

                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }



        [HttpPatch, Route("contract-customer/set-owner")]
        public IHttpActionResult SetOwner(int customerContractId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var cusCon = _customerContractService.Get(_ => _.Id == customerContractId);
                if (cusCon == null) return BadRequest("Customer contract not found");

                var listCusCon = _customerContractService.GetAll().Where(_ => _.ContractId == cusCon.ContractId);

                foreach (var item in listCusCon)
                {
                    if (item.Id == cusCon.Id) item.IsOwner = true;
                    else item.IsOwner = false;
                }

                _customerContractService.Update(listCusCon);

                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

    }
}
