using BusinessLogic.Define;
using DataAccess.Database;
using DataAccess.Entities;
using IdentityManager.Entities;
using IdentityManager.IdentityService;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace DormitoryUI.Controllers
{
    [RoutePrefix("report")]
    public class ProblemReportController : BaseController
    {
        protected HttpClient client;
        public readonly AccountAdapter _accountService;
        public readonly IApartmentService _apartmentService;
        public readonly IRoomService _roomService;

        public ProblemReportController(IEntityContext context, IApartmentService apartmentService,
            IRoomService roomService)
        {
            _accountService = new AccountAdapter(context);
            _apartmentService = apartmentService;
            _roomService = roomService;

            // get api
            client = new HttpClient();
            client.BaseAddress = new Uri("http://mapihelpdesk.unicode.edu.vn/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [HttpGet, Route("report-list")]
        [Authorize]
        public async Task<IHttpActionResult> GetAllReport()
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                _apartmentService.GetAll(_ => _.Rooms).Where(_ => _.AgencyId != null);
                List<Apartment> apartments = null;
                if (User.IsInRole(AccountType.ADMINISTRATOR.ToString()))
                {
                    apartments = _apartmentService.GetAll(_ => _.Rooms).Where(_ => _.AgencyId != null).ToList();
                }
                else if (User.IsInRole(AccountType.EMPLOYEE.ToString()))
                {
                    var emp = await _accountService.GetEmployeeByAccount(User.Identity.GetUserId());
                    apartments = _apartmentService.GetAll(_ => _.Rooms)
                        .Where(_ => _.AgencyId != null && _.BrandId == emp.BrandId).ToList();
                }
                else
                {
                    return Unauthorized();
                }


                List<object> reports = new List<object>();

                foreach (var apartment in apartments)
                {
                    HttpResponseMessage respone =
                        await client.GetAsync("request/agency_requests?agency_id=" + apartment.AgencyId);
                    OBRThree phuongServices = await respone.Content.ReadAsAsync<OBRThree>();


                    foreach (var item in phuongServices.ObjReturn)
                    {
                        var roomId = 0;

                        try
                        {
                            var tmp = item.request_description.Split(' ')[0];
                            item.request_description = item.request_description.Substring(tmp.Length + 1);
                            roomId = int.Parse(tmp);
                        }
                        catch (Exception)
                        {
                            roomId = 1;
                        }

                        reports.Add(new
                        {
                            apartment = new
                            {
                                id = apartment.Id,
                                name = apartment.Name
                            },
                            room = new
                            {
                                id = roomId,
                                name = _roomService.Get(_ => _.Id == roomId).Name
                            },
                            serviceName = item.service_name,
                            serviceItemName = item.service_item_name,
                            description = item.request_description,
                            createdDate = item.create_date,
                            status = new
                            {
                                id = item.request_status_value,
                                name = item.request_status
                            }
                        });
                    }
                }



                

                return Ok(reports);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
