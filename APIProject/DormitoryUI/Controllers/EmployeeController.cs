using BusinessLogic.Define;
using DataAccess.Database;
using DormitoryUI.ViewModels;
using IdentityManager.Entities;
using IdentityManager.IdentityService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DormitoryUI.Controllers
{
    [RoutePrefix("employee")]
    public class EmployeeController : BaseController
    {
        public readonly IEmployeeService _employeeService;
        private readonly AccountAdapter _accountAdapter;

        public EmployeeController(IEmployeeService employeeService, IEntityContext context)
        {
            _employeeService = employeeService;
            _accountAdapter = new AccountAdapter(context);
        }

        [HttpGet]
        [Route("all-employee")]
        public IHttpActionResult GetAllEmployee()
        {
            try
            {
                var employees = _employeeService.GetAll();
                return Ok(employees);
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
                var employees = _employeeService.Get(_ => _.Id == id);
                return Ok(employees);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPut, Route("")]
        public IHttpActionResult Update(EmployeeUpdateVM viewModel)
        {
            try
            {
                var emp = _employeeService.Get(_ => _.Id == viewModel.Id);

                if (emp == null) return BadRequest("Employee not found");

                emp.Phone = viewModel.Phone;
                emp.Fullname = viewModel.Fullname;
                emp.BrandId = viewModel.BrandId;
                emp.Email = viewModel.Email;
                

                _employeeService.Update(emp);
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

    }
}
