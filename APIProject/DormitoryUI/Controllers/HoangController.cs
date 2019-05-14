using BusinessLogic.Define;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DormitoryUI.Controllers
{
    [RoutePrefix("hoangapi")]
    public class HoangController : BaseController
    {
        private readonly IApartmentService _apartmentService;
        private readonly IRoomService _roomService;
        private readonly ICustomerService _customerService;

        public HoangController(IApartmentService apartmentService, 
            IRoomService roomService, ICustomerService customerService)
        {
            _apartmentService = apartmentService;
            _roomService = roomService;
            _customerService = customerService;
        }

        [HttpGet, Route("apartments")]
        public IHttpActionResult Get()
        {
            var list = _apartmentService.GetAll().Select(_ => new
            {
                _.Id,
                _.Name
            });

            return Ok(list);
        }


        [HttpGet, Route("rooms")]
        public IHttpActionResult GetRooms(int apartmentId)
        {
            var list = _roomService.GetAll().Where(_ => _.ApartmentId == apartmentId).Select(_ => new
            {
                _.Id,
                _.Name
            });

            return Ok(list);
        }

        [HttpGet, Route("clients")]
        public IHttpActionResult GetCustomers(int roomId)
        {
            var room = _roomService.Get(_ => _.Id == roomId, _ => _.Contracts
            .Select(__ =>__.CustomerContracts.Select(___ =>___.Customer)));
            if (room == null) return BadRequest("Room not found");
            if (room.Contracts.Count == 0) return Ok(new { clients = new List<object>() });
            var customers = room.Contracts.FirstOrDefault()
                .CustomerContracts.Select(_ => new
                {
                    _.Customer.Id,
                    _.Customer.Fullname
                });

            return Ok(customers);
        }
    }
}
