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

    [RoutePrefix("room")]
    public class RoomController : BaseController
    {
        public readonly IRoomService _roomService;
        public readonly IRoomTypeService _roomTypeService;
        public readonly AccountAdapter _accountService;

        public RoomController(IEntityContext context, IRoomService roomService, IRoomTypeService roomTypeService)
        {
            _accountService = new AccountAdapter(context);
            _roomService = roomService;
            _roomTypeService = roomTypeService;
        }
        

        [HttpGet, Route("")]
        public IHttpActionResult GetRoom(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var result = _roomService.Get(_ => _.Id == id, _ => _.RoomType, _ => _.Apartment, _ => _.Contracts);

                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet, Route("all-brand-room")]
        public IHttpActionResult GetRoomByBrand(int brandId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var result = _roomService.GetAll(z => z.Apartment.Brand, _ => _.RoomType, _ => _.Contracts)
                    .Where(z => z.Apartment.BrandId == brandId);
                if (result == null) return BadRequest("Room not found");

                return Ok(result);
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

                var result = _roomService.Get(_ => _.Id == id);
                if (result == null) return BadRequest("Room not found");

                _roomService.Delete(result);


                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPost, Route("")]
        public IHttpActionResult CreateRoom(RoomCreateVM viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                _roomService.Create(ModelMapper.ConvertToModel(viewModel));

                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet, Route("all-room")]
        [Authorize]
        public async Task<IHttpActionResult> GetAll()
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

               
                List<Room> rooms;

                if (User.IsInRole(AccountType.ADMINISTRATOR.ToString()))
                {
                    rooms = _roomService.GetAll(_ => _.RoomType, _ => _.Apartment).ToList();
                }
                else
                {
                    var emp = await _accountService.GetEmployeeByAccount(User.Identity.GetUserId());
                    rooms = _roomService.GetAll(_ => _.RoomType, _ => _.Apartment)
                        .Where(_ => _.Apartment.BrandId == emp.BrandId).ToList();
                }

                return Ok(rooms);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPut, Route("")]
        public IHttpActionResult Update(RoomUpdateVM viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var room = _roomService.Get(z => z.Id == viewModel.Id);
                if (room == null) return BadRequest("Room not found");

                room.Name = viewModel.Name;
                room.RoomTypeId = viewModel.RoomTypeId;
                room.Status = viewModel.Status;
                room.ApartmentId = viewModel.ApartmentId;

                _roomService.Update(room);

                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

    }
}
