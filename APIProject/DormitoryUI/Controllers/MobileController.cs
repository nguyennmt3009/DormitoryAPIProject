using BusinessLogic.Define;
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
    [RoutePrefix("mobile")]
    public class MobileController : BaseController
    {
        public readonly ICustomerService _customerService;
        public readonly IContractService _contractService;
        public readonly IRoomService _roomService;

        protected HttpClient client;

        public MobileController(ICustomerService customerService, 
            IContractService contractService, IRoomService roomService)
        {
            _customerService = customerService;
            _contractService = contractService;
            _roomService = roomService;
            client = new HttpClient();
            client.BaseAddress = new Uri("http://mapihelpdesk.unicode.edu.vn/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [HttpGet, Route("contract/all-contract/{customerId}")]
        public IHttpActionResult GetAllCustomerContract(int customerId)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("Invalid customerId");

                var customer = _customerService.Get(z => z.Id == customerId, 
                    z => z.CustomerContracts.Select(a => a.Contract));
                var contracts = customer.CustomerContracts.Select(z => z.Contract);
                    

                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet, Route("report/{id}")]
        public async Task<IHttpActionResult> GetReport(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Customer not found");

                HttpResponseMessage respone = await client.GetAsync("agency/serviceITSupport/198");
                OBROne phuongServices = await respone.Content.ReadAsAsync<OBROne>();
                OBRTwo objReturnTwo;
                foreach (var item in phuongServices.ObjReturn)
                {
                    respone = await client.GetAsync("agency/serviceItem/" + item.service_it_support_id);
                    objReturnTwo = await respone.Content.ReadAsAsync<OBRTwo>();

                    item.ServiceItems = objReturnTwo.ObjReturn;
                }

                var listPhuongService = new List<object>();

                phuongServices.ObjReturn.ForEach(_ => {
                    listPhuongService.Add(new
                    {
                        id = _.service_it_support_id,
                        name = _.service_name,
                        serviceItems = _.ServiceItems
                    });
                });

                var customer = _customerService.Get(_ => _.Id == id, _ => _.CustomerContracts,
                    _ => _.CustomerContracts.Select(__ => __.Contract.Room.Apartment));
                var sysApartments = customer.CustomerContracts.Select(_ => _.Contract.Room.Apartment);

                var apartments = new List<object>();
                sysApartments.ToList().ForEach(sysa =>
                {
                    var sysRooms = new List<object>();

                    sysa.Rooms.ToList().ForEach(sysr => {
                        sysRooms.Add(new {
                            id = sysr.Id,
                            name = sysr.Name
                        });
                    });

                    apartments.Add(new
                    {
                        id = sysa.Id,
                        name = sysa.Name,
                        rooms = sysRooms,
                        services = listPhuongService
                    });
                });

                return Ok(apartments);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPost, Route("report")]
        public IHttpActionResult CreateReport(int roomId, int serviceItemId, string description)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Parameter invalid");

                //HttpResponseMessage respone = await client.GetAsync("agency/serviceITSupport/198");
                
                return Ok("Báo cáo sự cố thành công");
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

    }

    #region PhuongService

    public class PhuongService
    {
        public int service_it_support_id { get; set; }
        public string service_name { get; set; }
        public List<PhuongServiceItem> ServiceItems { get; set; }
    }

    public class OBROne
    {
        public List<PhuongService> ObjReturn { get; set; }
    }

    public class OBRTwo
    {
        public List<PhuongServiceItem> ObjReturn { get; set; }
    }

    public class PhuongServiceItem
    {
        public int ServiceItemId { get; set; }
        public string ServiceItemName { get; set; }
    }
    #endregion
}
