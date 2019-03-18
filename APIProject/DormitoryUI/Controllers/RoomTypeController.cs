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

    [RoutePrefix("roomtype")]
    public class RoomTypeController : BaseController
    {
        public readonly IRoomTypeService _roomTypeService;
        public readonly AccountAdapter _accountService;

        public RoomTypeController(IEntityContext context, IRoomTypeService roomTypeService)
        {
            _accountService = new AccountAdapter(context);
            _roomTypeService = roomTypeService;
        }

        [HttpPost, Route("")]
        public IHttpActionResult CreateRoom(RoomTypeCreateVM viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                _roomTypeService.Create(ModelMapper.ConvertToModel(viewModel));

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

                var result = _roomTypeService.Get(_ => _.Id == id);
                if (result == null) return BadRequest("Room type not found");

                _roomTypeService.Delete(result);


                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet, Route("all-apartment-roomtype")]
        public IHttpActionResult GetRoomTypeOfApartment(int apartmentId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var result = _roomTypeService.GetAll(_ => _.Apartment)
                    .Where(_ => _.ApartmentId == apartmentId).ToList()
                    .Select(_ => ModelMapper.ConvertToViewModel(_));

                return Ok(result);
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
                if (!ModelState.IsValid)
                    return BadRequest();

                var result = _roomTypeService.Get(_ => _.Id == id);

                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet, Route("all-brand-roomtype")]
        public IHttpActionResult GetAllOfBrand(int brandId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var result = _roomTypeService.GetAll(_ => _.Apartment.Brand)
                    .Where(_ => _.Apartment.Brand.Id == brandId);

                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }


        [HttpGet, Route("all-roomtype")]
        [Authorize]
        public async Task<IHttpActionResult> GetAll()
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                List<RoomType> roomTypes;

                if (User.IsInRole(AccountType.ADMINISTRATOR.ToString()))
                {
                    roomTypes = _roomTypeService.GetAll(_ => _.Apartment).ToList();
                }
                else
                {
                    var emp = await _accountService.GetEmployeeByAccount(User.Identity.GetUserId());
                    roomTypes = _roomTypeService.GetAll(_ => _.Apartment)
                        .Where(_ => _.Apartment.BrandId == emp.BrandId).ToList();
                }
                return Ok(roomTypes);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPut, Route("")]
        public IHttpActionResult Update(RoomTypeUpdateVM viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var roomType = _roomTypeService.Get(_ => _.Id == viewModel.Id);

                if (roomType == null) return BadRequest("Room Type not found");

                roomType.Name = viewModel.Name;
                roomType.Description = viewModel.Description;
                roomType.ApartmentId = viewModel.ApartmentId;

                _roomTypeService.Update(roomType);

                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        


    }
}
