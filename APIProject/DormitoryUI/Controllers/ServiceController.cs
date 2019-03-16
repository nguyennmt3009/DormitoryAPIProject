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
    [RoutePrefix ("service")]
    public class ServiceController : BaseController
    {
        public readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
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

                var result = _serviceService.GetAll(_ => _.BrandServices);

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

                _serviceService.Update(ModelMapper.ConvertToModel(viewModel));

                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
