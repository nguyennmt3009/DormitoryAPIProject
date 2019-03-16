using BusinessLogic.Define;
using DataAccess.Entities;
using DormitoryUI.ViewModels;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DormitoryUI.Controllers
{
    [RoutePrefix("apartment")]
    //[Authorize(Roles = "EMPLOYEE")]
    public class ApartmentController : BaseController
    {
        public readonly IApartmentService _apartmentService;
        public readonly IBrandService _brandService;

        public ApartmentController(IApartmentService apartmentService, IBrandService brandService)
        {
            _apartmentService = apartmentService;
            _brandService = brandService;
        }

        [HttpGet]
        //[SwaggerResponse(HttpStatusCode.OK, Type = typeof(Apartment))]
        [Route("")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();
                var result = _apartmentService.Get(_ => _.Id == id, _ => _.RoomTypes, _ => _.Rooms);
                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet]
        [Route("all-apartment")]
        public IHttpActionResult GetAll()
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();
                
                var result = _apartmentService.GetAll(_ => _.Rooms, _ => _.RoomTypes).ToList();


                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet]
        [Route("all-brand-apartment")]
        //[SwaggerResponse(HttpStatusCode.OK, Type = typeof(List<Apartment>))]
        public IHttpActionResult GetAllOfBrand(int brandId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                if (_brandService.Get(_ => _.Id == brandId) == null)
                    return BadRequest("Brand is not existed");


                var result = _apartmentService.GetAll(_ => _.Brand)
                    .Where(_ => _.BrandId == brandId).ToList();


                return Ok(ModelMapper.ConvertToViewModel(result));
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateApartment(ApartmentCreateVM viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                if(_brandService.Get(_ => _.Id == viewModel.BrandId) == null)
                {
                    return BadRequest("Brand not found");
                }

                _apartmentService.Create(ModelMapper.ConvertToModel(viewModel));
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

        }

        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateApartment(ApartmentUpdateVM viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();
                var a = _apartmentService.Get(_ => _.Id == viewModel.Id);
                a.Location = viewModel.Location;
                a.Name = viewModel.Name;
                a.AgencyId = viewModel.Agency;

                _apartmentService.Update(a);
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
