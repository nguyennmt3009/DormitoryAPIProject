using BusinessLogic.Define;
using DataAccess.Database;
using DataAccess.Entities;
using DormitoryUI.ViewModels;
using IdentityManager.Entities;
using IdentityManager.IdentityService;
using Microsoft.AspNet.Identity;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace DormitoryUI.Controllers
{
    [RoutePrefix("apartment")]
    //[Authorize(Roles = "EMPLOYEE")]
    public class ApartmentController : BaseController
    {
        public readonly IApartmentService _apartmentService;
        public readonly IBrandService _brandService;
        public readonly AccountAdapter _accountService;

        public ApartmentController(IEntityContext context, IApartmentService apartmentService, IBrandService brandService)
        {
            _accountService = new AccountAdapter(context);
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
        [Authorize]
        public async Task<IHttpActionResult> GetAll()
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();
                List<Apartment> result = null;
                if (User.IsInRole(AccountType.ADMINISTRATOR.ToString()))
                {
                    result = _apartmentService.GetAll(_ => _.Rooms, _ => _.RoomTypes, _ => _.Brand).ToList();
                } else 
                {
                    var emp = await _accountService.GetEmployeeByAccount(User.Identity.GetUserId());
                    result = _apartmentService.GetAll(_ => _.Rooms, _ => _.RoomTypes)
                        .Where(_ => _.BrandId == emp.BrandId).ToList();
                }
                
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

        [HttpDelete]
        [Route("")]
        public IHttpActionResult DeleteApartment(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();
                var a = _apartmentService.Get(_ => _.Id == id);
                if (a == null)
                {
                    return BadRequest("Apartmnent not found");
                }

                _apartmentService.Delete(a);
                return Ok();
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
