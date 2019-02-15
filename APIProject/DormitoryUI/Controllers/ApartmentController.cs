using BusinessLogic.Define;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DormitoryUI.Controllers
{
    public class ApartmentController : ApiController
    {
        public readonly IApartmentService _apartmentService;

        public ApartmentController(IApartmentService apartmentService)
        {
            _apartmentService = apartmentService;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok("test");
        }
    }
}
