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

    [RoutePrefix("room")]
    public class RoomController : BaseController
    {
        public readonly IRoomService _roomService;
        public readonly IRoomTypeService _roomTypeService;

        public RoomController(IRoomService roomService, IRoomTypeService roomTypeService)
        {
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
        public IHttpActionResult GetAll()
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var result = _roomService.GetAll(_ => _.RoomType, _ => _.Apartment, _ => _.Contracts);

                return Ok(result);
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
