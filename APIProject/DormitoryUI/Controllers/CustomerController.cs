using BusinessLogic.Define;
using DataAccess.Entities;
using DormitoryUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DormitoryUI.Controllers
{
    public class CustomerController : ApiController
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public IHttpActionResult Create(CustomerCreateVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _customerService.Create(new Customer
                {
                    Birthdate = model.Birthdate,
                    Email = model.Email,
                    Fullname = model.Fullname,
                    Phone = model.Phone
                });
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
