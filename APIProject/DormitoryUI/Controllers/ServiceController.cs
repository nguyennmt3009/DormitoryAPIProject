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
    [RoutePrefix ("service")]
    public class ServiceController : BaseController
    {
        public readonly IServiceService _serviceService;
        public readonly AccountAdapter _accountService;

        public ServiceController(IEntityContext context, IServiceService serviceService)
        {
            _accountService = new AccountAdapter(context);
            _serviceService = serviceService;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetService (int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var result = _serviceService.Get(_ => _.Id == id);

                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Create(ServiceCreateVM viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                _serviceService.Create(ModelMapper.ConvertToModel(viewModel));

                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet]
        [Route("all-service")]
        public IHttpActionResult GetAll()
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                List<Service> services = _serviceService.GetAll(_ => _.BrandServices).ToList();
                var result = services.Where(_ => _.BrandServices.Count == 0).Select(_ => new {
                    _.Id,
                    _.Name
                });
                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPut]
        [Route("")]
        public IHttpActionResult Update(ServiceUpdateVM viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var service = _serviceService.Get(_ => _.Id == viewModel.Id);
                if (service == null) return BadRequest("Service not found");

                service.Name = viewModel.Name;

                _serviceService.Update(service);

                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpDelete]
        [Route("")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                Service service = _serviceService.Get(_ => _.Id == id);
                if (service == null) return BadRequest("Service not found");

                _serviceService.Update(service);

                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
