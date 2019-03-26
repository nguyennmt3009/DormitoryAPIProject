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
    [RoutePrefix("bill-detail")]
    public class BillDetailController : BaseController
    {
        public readonly IBillDetailService _billDetailService;
        public readonly IBillService _billService;
        public readonly IBrandServiceService _brandServiceService;

        public BillDetailController(IBillDetailService billDetailService, 
            IBillService billService, IBrandServiceService brandServiceService)
        {
            _billDetailService = billDetailService;
            _billService = billService;
            _brandServiceService = brandServiceService;
        }

        [HttpGet, Route("")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var result = _billDetailService.Get(_ => _.Id == id);

                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }


        /// <summary>
        /// Mỗi lần tạo bill detail thì sẽ tự cộng tiền vào bill
        /// Giá của bill detail lấy từ giá brand service hiện tại
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost, Route("")]
        public IHttpActionResult Create(BillDetailCreateVM viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var bill = _billService.Get(_ => _.Id == viewModel.BillId);
                var brandService = _brandServiceService.Get(_ => _.Id == viewModel.BrandServiceId);

                if (bill == null) return BadRequest("Bill not found");
                if (brandService == null) return BadRequest("Brand service not found");


                var billDetail = ModelMapper.ConvertToModel(viewModel);
                billDetail.Price = brandService.Price;
                _billDetailService.Create(billDetail);


                bill.TotalAmount += viewModel.Quantity * brandService.Price;
                _billService.Update(bill);

                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet, Route("all-bill-detail")]
        public IHttpActionResult GetAll()
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var result = _billDetailService.GetAll();

                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet, Route("all-billDetail-byBill")]
        public IHttpActionResult GetAllByBill(int billId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();
                var bill = _billService.Get(_ => _.Id == billId);
                if (bill == null) return BadRequest("Bill not found");

                var result = _billDetailService.GetAll(_ => _.BrandService.Service)
                    .Where(_ => _.BillId == billId).ToList();

                return Ok(ModelMapper.ConvertToViewModel(result));
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPut, Route("")]
        public IHttpActionResult Update(BillDetailUpdateVM viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var billDetail = _billDetailService.Get(_ => _.Id == viewModel.Id, _ => _.Bill, _ => _.BrandService);
                if (billDetail == null) return BadRequest("Bill detail not found");
                


                billDetail.Bill.TotalAmount = billDetail.Bill.TotalAmount 
                    + (viewModel.Quantity - billDetail.Quantity) * billDetail.Price;

                _billService.Update(billDetail.Bill);

                billDetail.Quantity = viewModel.Quantity;

                _billDetailService.Update(billDetail);

                return Ok();
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

                var billDetail = _billDetailService.Get(_ => _.Id == id);
                if (billDetail == null) return BadRequest("Bill detail not found");

                _billDetailService.Delete(billDetail);

                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
