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
    public class BrandController : BaseController
    {
        public readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpPost, Route("brand")]
        public IHttpActionResult CreateBrand(BrandCreateVM viewModel)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();

                _brandService.Create(new Brand()
                {
                    Name = viewModel.Name
                });

                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet]
        [Route("brand")]
        public IHttpActionResult Get(int brandId)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();

                return Ok(_brandService.Get(_ => _.Id == brandId, _ => _.Apartments));
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }


        [HttpGet]
        [Route("all-brand")]
        public IHttpActionResult GetAll()
        {
            try
            {
                return Ok(_brandService.GetAll(_ => _.Apartments, _ => _.BrandServices));
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPut]
        [Route("brand")]
        public IHttpActionResult UpdateBrand(BrandUpdateVM viewModel)
        {
            try
            {
                _brandService.Update(ModelMapper.ConvertToModel(viewModel));
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
