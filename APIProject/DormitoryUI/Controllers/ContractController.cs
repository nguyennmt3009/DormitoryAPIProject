using BusinessLogic.Define;
using DataAccess.Database;
using DataAccess.Entities;
using DormitoryUI.ViewModels;
using IdentityManager.Entities;
using IdentityManager.IdentityService;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace DormitoryUI.Controllers
{
    public class PhuongResponse
    {
        [JsonProperty(PropertyName = "result-code")]
        public int resultCode { get; set; }
        public string message { get; set; }
        public bool success { get; set; }
        public string error { get; set; }
        public object data { get; set; }

    }
    
    public class ContractController : BaseController
    {
        public readonly IContractService _contractService;
        public readonly ICustomerContractService _customerContractService;
        public readonly AccountAdapter _accountService;

        public ContractController(IEntityContext context, IContractService contractService, ICustomerContractService customerContractService)
        {
            _accountService = new AccountAdapter(context);
            _contractService = contractService;
            _customerContractService = customerContractService;
        }

        [HttpGet, Route("transaction")]
        public IHttpActionResult GetTransaction(int customer_id, string from, string to)
        {
            return Ok(new PhuongResponse {
                resultCode = 1,
                message = "Mieo mieo",
                success = true,
                error = null,
                data = new List<object>
                {
                    new
                    {
                        amount = 6000000,
                        billId = 1,
                        date = "11/02/2018",
                        isDebit = false,
                    },
                    new
                    {
                        amount = 2400000,
                        billId = 8,
                        date = "15/03/2018",
                        isDebit = true,
                    },
                    new
                    {
                        amount = 3000000,
                        billId = 4,
                        date = "11/04/2018",
                        isDebit = false,
                    },
                    new
                    {
                        amount = 3600000,
                        billId = 2,
                        date = "21/05/2019",
                        isDebit = true,
                    }
                }
            });
        }

        [HttpGet, Route("contract")]
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

        [HttpGet, Route("contract/all-brand-contract")]
        public IHttpActionResult GetByBrand(int brandId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var result = _contractService.GetAll(_ => _.Room.Apartment.Brand)
                    .Where(_ => _.Room.Apartment.BrandId == brandId);

                if (result == null) return BadRequest("Contract not found");

                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet, Route("contract/active-contract-by-room")]
        public IHttpActionResult GetActiveByRoom(int roomId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var result = _contractService.GetAll(_ => _.Room, _ => _.CustomerContracts.Select(__ => __.Customer))
                    .Where(_ => _.RoomId == roomId && _.Status);

                if (result == null) return BadRequest("Contract not found");

                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPost, Route("contract")]
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

        [HttpGet, Route("contract/all-contract")]
        [Authorize]
        public async Task<IHttpActionResult> GetAll()
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                List<Contract> result = null;
                if (User.IsInRole(AccountType.ADMINISTRATOR.ToString()))
                {
                    result = _contractService.GetAll(_ => _.Room.Apartment).ToList();
                }
                else
                {
                    var emp = await _accountService.GetEmployeeByAccount(User.Identity.GetUserId());
                    result = _contractService.GetAll(_ => _.Room.Apartment)
                        .Where(_ => _.Room.Apartment.BrandId == emp.BrandId && _.Status).ToList();
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        //[HttpGet, Route("mobile/all-contract/{customerId}")]
        //public IHttpActionResult GetAllByCustomer(int customerId)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //            return BadRequest();

        //        var result = _contractService.GetAll(_ => _.Room);

        //        return Ok(result);
        //    }
        //    catch (Exception e)
        //    {
        //        return InternalServerError(e);
        //    }
        //}


        [HttpPut, Route("contract")]
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

        [HttpPost, Route("contract/contract-customer")]
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

        [HttpGet, Route("contract/all-contract-customer")]
        public IHttpActionResult GetAllContractCustomer(int contractId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var result = _customerContractService.GetAll(_ => _.Customer)
                    .Where(_ => _.ContractId == contractId);

                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }



        [HttpPatch, Route("contract/contract-customer/set-owner")]
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
