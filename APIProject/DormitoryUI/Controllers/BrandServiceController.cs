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
    [RoutePrefix("brand-service")]
    public class BrandServiceController : BaseController
    {
        public readonly IBrandServiceService _brandServiceService;

        public BrandServiceController(IBrandServiceService brandServiceService)
        {
            _brandServiceService = brandServiceService;
        }


        [HttpGet]
        [Route("")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var result = _brandServiceService.Get(_ => _.Id == id, _ => _.Brand, _ => _.Service);

                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet]
        [Route("all-brand-service")]
        public IHttpActionResult GetAll()
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var result = _brandServiceService.GetAll(_ => _.Service, _ => _.Brand);

                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Create(BrandServiceCreateVM viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                _brandServiceService.Create(ModelMapper.ConvertToModel(viewModel));

                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPut, Route("")]
        public IHttpActionResult Update(BrandServiceUpdateVM viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                _brandServiceService.Update(ModelMapper.ConvertToModel(viewModel));

                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpDelete, Route("")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();


                var entity = _brandServiceService.Get(_ => _.Id == id);
                if (entity == null) return BadRequest("Not found!");

                _brandServiceService.Delete(entity);

                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
