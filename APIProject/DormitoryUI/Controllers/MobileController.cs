using BusinessLogic.Define;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace DormitoryUI.Controllers
{
    [RoutePrefix("mobile")]
    public class MobileController : BaseController
    {
        public readonly ICustomerService _customerService;
        public readonly ICustomerContractService _customerContractService;
        public readonly IContractService _contractService;
        public readonly IRoomService _roomService;

        protected HttpClient client;

        public MobileController(ICustomerService customerService, 
            IContractService contractService, IRoomService roomService,
            ICustomerContractService customerContractService)
        {
            _customerContractService = customerContractService;
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

                var contracts = _contractService.GetAll(z => z.CustomerContracts.Select(zz => zz.Customer),
                    z => z.Room.Apartment.Brand).Where(z => z.CustomerContracts
                    .FirstOrDefault(c => c.CustomerId == customerId) != null);

                var contractList = new List<object>();

                foreach (var item in contracts)
                {
                    var owner = item.CustomerContracts.FirstOrDefault(z => z.IsOwner);

                    contractList.Add(new
                    {
                        ownerName = owner == null ? "Chưa có chủ phòng" : owner.Customer.Fullname,
                        listCustomerName = item.CustomerContracts.Select(z => z.Customer.Fullname),
                        contractId = item.Id,   
                        createdDate = item.CreatedDate.ToString("dd/MM/yyyy"),
                        dueDate = item.DueDate.ToString("dd/MM/yyyy"),
                        deposit = item.Deposit.ToString("N"),
                        monthlyFee = item.DueAmount.ToString("N"),
                        roomName = item.Room.Name,
                        apartmentName = item.Room.Apartment.Name,
                        brandName = item.Room.Apartment.Brand.Name,
                    });
                }

                return Ok(contractList);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet, Route("report-list/{customerId}")]
        public async Task<IHttpActionResult> GetAllReport(int customerId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Customer not found");

                var customer = _customerService.Get(_ => _.Id == customerId, _ => _.CustomerContracts,
                    _ => _.CustomerContracts.Select(__ => __.Contract.Room.Apartment));

                if (customer == null) return BadRequest("Customer not found");

                var apartment = customer.CustomerContracts.FirstOrDefault().Contract.Room.Apartment;

                if (apartment.AgencyId == null) return BadRequest("Chưa đăng ký sự cố");

                HttpResponseMessage respone =
                    await client.GetAsync("request/agency_requests?agency_id=" + apartment.AgencyId);
                OBRThree phuongServices = await respone.Content.ReadAsAsync<OBRThree>();

                List<object> result = new List<object>();

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

                    result.Add(new
                    {
                        apartment = new
                        {
                            id = apartment.Id,
                            name = apartment.Name
                        },
                        roomName = _roomService.Get(_ => _.Id == roomId).Name,
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

                return Ok(result);
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
        public async Task<IHttpActionResult> CreateReport(int roomId, int serviceItemId, string description)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Parameter invalid");

                var room = _roomService.Get(z => z.Id == roomId, z => z.Apartment);

                if (room == null) return BadRequest("Room not found");

                var report = JsonConvert.SerializeObject(new
                {
                    agency_id = room.Apartment.AgencyId ?? 198,
                    service_item_id = serviceItemId,
                    request_name = room.Name,
                    description = roomId + " " + description
                });

                var buffer = Encoding.UTF8.GetBytes(report);
                var byteContent = new ByteArrayContent(buffer);


                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage respone = await client.PostAsync("agency/create_request_hang", byteContent);

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

    public class OBRThree
    {
        public List<PhuongReport> ObjReturn { get; set; }
    }

    public class PhuongReport
    {
        public string service_name { get; set; }
        public string service_item_name { get; set; }
        public string request_description { get; set; }
        public string create_date { get; set; }
        public int request_status_value { get; set; }
        public string request_status { get; set; }
    }

    public enum PhuongServiceStatus
    {
        [Display(Name = "Chờ nhân viên xử lý")]
        Pending = 1,
        [Display(Name = "Đang xử lý")]
        Processing = 2,
        [Display(Name = "Hoàn thành")]
        Done = 3,
        [Display(Name = "Hủy bỏ")]
        Cancel = 4,
        [Display(Name = "Tạo mới")]
        New = 5,
        [Display(Name = "Chờ hủy")]
        WaitingCancel = 6,
        [Display(Name = "Chờ xác nhận hoàn thành")]
        WaitingDone = 7,
        
    }

    #endregion
}
