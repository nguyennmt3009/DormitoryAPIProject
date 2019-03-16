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
    //[RoutePrefix("api/user")]
    public class UserController : BaseController
    {
        private readonly ICustomerService _customerService;

        public UserController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        //[Route("{id}")]
        //[HttpGet]
        //[SwaggerResponse(HttpStatusCode.OK, Type = typeof(Customer))]
        //[SwaggerResponse(HttpStatusCode.NotFound)]
        //[SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(string))]


        [HttpGet]
        [Route("{userId}/bill-detail")]
        public IHttpActionResult GetBills(int? userId)
        {
            return Ok(new List<object>
            {
                new
                {
                    id = 12156,
                    month = "tháng 2",
                    room = "Phòng 305",
                    apartment = "Chung cư AB",
                    amount = 3500000,
                    status = "Chưa thanh toán",
                    createdDate = "12/02/2018",
                    serviceList = new List<object>
                    {
                        new {
                            name = "Tiền nhà",
                            quantity = 1,
                            price= 3000000,
                        },
                        new {
                            name = "Internet",
                            quantity = 1,
                            price= 500000,
                        }
                    }
                }, 
                new
                {
                    id = 1354,
                    month = "tháng 2",
                    room = "Phòng 5",
                    apartment = "Khu nhà XY",
                    amount = 3600000,
                    status = "Đã thanh toán",
                    createdDate = "01/02/2018",
                    serviceList = new List<object>
                    {
                        new {
                            name = "Tiền nhà",
                            quantity = 1,
                            price= 3000000,
                        },
                        new {
                            name = "Internet",
                            quantity = 1,
                            price= 500000,
                        },
                        new
                        {
                            name = "Giặt ủi",
                            quantity = 1,
                            price= 100000,
                        }
                    }
                }

            });
        }


        [HttpGet]
        [Route("{userId}/bill-detail/{billId}")]
        public IHttpActionResult GetBillDetail(int? userId, int? billId)
        {

            return Ok(new
            {
                id = 12156,
                month = "tháng 2",
                room = "Phòng 305",
                apartment = "Chung cư AB",
                amount = 3500000,
                status = "Chưa thanh toán",
                createdDate = "12/02/2018",
                serviceList = new List<object>
                {
                    new {
                        name = "Tiền nhà",
                        quantity = 1,
                        price= 3000000,
                    },
                    new {
                        name = "Internet",
                        quantity = 1,
                        price= 500000,
                    }
                }
            });

        }

    }


}


