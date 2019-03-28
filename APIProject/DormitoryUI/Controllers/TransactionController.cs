//using BusinessLogic.Define;
//using DataAccess.Entities;
//using DormitoryUI.ViewModels;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Web.Http;

//namespace DormitoryUI.Controllers
//{
//    [RoutePrefix("third-party")]
//    public class TransactionController : BaseController
//    {
//        private readonly ITransactionService _transactionService;
//        private readonly IAccountCustomerService _accountCustomerService;
//        private readonly IAccountPaymentService _accountPaymentService;

//        public TransactionController(ITransactionService transactionService, 
//            IAccountCustomerService accountCustomerService, IAccountPaymentService accountPaymentService)
//        {
//            _transactionService = transactionService;
//            _accountCustomerService = accountCustomerService;
//            _accountPaymentService = accountPaymentService;
//        }

//        [HttpPost, Route("customer")]
//        public IHttpActionResult Create(AccountCustomerCreateVM request)
//        {
//            var customer = new AccountCustomer
//            {
//                CustomerId = request.Id,
//                Status = true,
//            };
//            _accountCustomerService.Create(customer);
//            var account = new AccountPayment
//            {
//                Amount = request.Amount,
//                AccountCustomerId = customer.Id,
//                Type = 1,
//            };
//            _accountPaymentService.Create(account);
//            return Ok(new CommonResponse<string>{
//                ResultCode = 200,
//                Message = "No error",
//                Success = true,
//                Error = null,
//                Data = null
//            });
//        }

//        [HttpGet, Route("customer")]
//        public IHttpActionResult Get(int customer_id)
//        {
//            return Ok();
//        }
//    }
//}
