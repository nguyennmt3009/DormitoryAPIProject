using BusinessLogic.Define;
using DataAccess.Entities;
using DormitoryUI.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace DormitoryUI.Controllers
{

    [RoutePrefix("")]
    public class BillController : BaseController
    {
        public readonly IBillService _billService;
        public readonly IContractService _contractService;
        public readonly IBillDetailService _billDetailService;
        public readonly ICustomerService _customerService;
        protected HttpClient client;
        public BillController(IBillService billService, IContractService contractService, 
            IBillDetailService billDetailService, ICustomerService customerService)
        {
            _billService = billService;
            _contractService = contractService;
            _billDetailService = billDetailService;
            _customerService = customerService;

            //----- get api
            client = new HttpClient();
            client.BaseAddress = new Uri("http://apicrm.unicode.edu.vn/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [HttpGet, Route("bill")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var result = _billService.Get(_ => _.Id == id, _ => _.Contract, _ => _.BillDetails);

                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        /// <summary>
        /// Mỗi lần tạo bill sẽ tạo luôn 1 record bill detail là tiền nhà
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost, Route("bill")]
        public IHttpActionResult Create(BillCreateVM viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();
                var bill = ModelMapper.ConvertToModel(viewModel);
                _billService.Create(bill);
                var dueAmount = _contractService.Get(_ => _.Id == bill.ContractId).DueAmount;

                _billDetailService.Create(new DataAccess.Entities.BillDetail
                {
                    BillId = bill.Id,
                    CreatedDate = DateTimeOffset.Now,
                    Price = dueAmount,
                    Quantity = 1,
                    IsBuildingRent = true
                });

                bill.TotalAmount += dueAmount;
                _billService.Update(bill);

                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet, Route("bill/all-bill")]
        public IHttpActionResult GetAll()
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var result = _billService.GetAll(_ => _.Contract, _ => _.BillDetails);

                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPut, Route("bill")]
        public IHttpActionResult Update(BillUpdateVM viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                _billService.Update(ModelMapper.ConvertToModel(viewModel));

                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }


        [HttpPost, Route("mobile/payment-bill")]
        public async Task<IHttpActionResult> BillPayment(int customerId, int billId) // <~~ CustomerId
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Customer not found");
                }

                HttpResponseMessage respone = await client.GetAsync("customer?customer_id=" + customerId);
                PhuongTransactionData data = await respone.Content.ReadAsAsync<PhuongTransactionData>();

                if (data.data == null) return BadRequest("Tài khoản không tồn tại");

                var bill = _billService.Get(_ => _.Id == billId);
                if (bill == null) return BadRequest("Bill not found"); 


                if (data.data.list_account.amount_balance < bill.TotalAmount)
                    return Ok("Bạn không đủ tiền để thực hiện giao dịch này");

                if (data.data.list_account.status == "Chưa kích hoạt")
                    return Ok("Tài khoản chưa kích hoạt");


                var transaction = JsonConvert.SerializeObject(new
                {
                    customer_id = customerId,
                    amount = bill.TotalAmount,
                    date = DateTimeOffset.Now,
                    bill_id = bill.Id,
                    is_debit = false
                });

                var buffer = Encoding.UTF8.GetBytes(transaction);
                var byteContent = new ByteArrayContent(buffer);


                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                respone = await client.PostAsync("transaction", byteContent);

                bill.Status = true;
                _billService.Update(bill);

                return Ok("Payment successful");
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }


        [HttpGet, Route("mobile/all-bill/{order}/{customerId}")]
        public IHttpActionResult GetBills(string order, int customerId)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var customer = _customerService.Get(x => x.Id == customerId,
                    _ => _.CustomerContracts.Select(y => y.Contract.Bills
                    .Select(z => z.BillDetails.Select(a => a.BrandService.Service))),
                    _ => _.CustomerContracts.Select(y => y.Contract.Room.Apartment));

                if (customer == null) return BadRequest("Not found customer");

                //List<Bill> bills = new List<Bill>();

                var bills = customer.CustomerContracts.Select(x => x.Contract.Bills)
                    .Aggregate(new List<Bill>(), (a, b) => a.Concat(b).ToList());
                
                bills = bills.OrderByDescending(_ => _.CreatedDate).ToList();

                if(order == "status=false")
                {
                    return Ok(ModelMapper.ConvertToViewModel1(bills.Where(x => !x.Status).ToList()));
                }
                if (order == "status=true")
                {
                    return Ok(ModelMapper.ConvertToViewModel1(bills.Where(x => x.Status).ToList()));
                }


                return Ok(ModelMapper.ConvertToViewModel1(bills));
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }


    public class PhuongTransactionUser
    {
        public int customer_id { get; set; }
        public PhuongTransactionAccount list_account { get; set; }
    }

    public class PhuongTransactionAccount
    {
        public int customer_id { get; set; }
        public decimal amount_balance { get; set; }
        public string status { get; set; }
    }
    public class PhuongTransactionData
    {
        public PhuongTransactionUser data { get; set; }
    }
}
