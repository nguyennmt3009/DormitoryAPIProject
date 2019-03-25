using BusinessLogic.Define;
using DataAccess.Database;
using DataAccess.Entities;
using DormitoryUI.ViewModels;
using IdentityManager.Entities;
using IdentityManager.IdentityService;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace DormitoryUI.Controllers
{
    [RoutePrefix("brand-service")]
    public class BrandServiceController : BaseController
    {
        public readonly IBrandServiceService _brandServiceService;
        public readonly AccountAdapter _accountService;

        public BrandServiceController(IEntityContext context, IBrandServiceService brandServiceService)
        {
            _accountService = new AccountAdapter(context);
            _brandServiceService = brandServiceService;
        }


        [HttpGet]
        [Route("")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var result = _brandServiceService.Get(_ => _.Id == id, _ => _.Brand, _ => _.Service);

                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet]
        [Route("all-brand-service")]
        [Authorize]
        public async Task<IHttpActionResult> GetAll()
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                List<BrandService> result = null;

                if (User.IsInRole(AccountType.ADMINISTRATOR.ToString()))
                {
                    result = _brandServiceService.GetAll(_ => _.Service, _ => _.Brand).ToList();
                }
                else
                {
                    var emp = await _accountService.GetEmployeeByAccount(User.Identity.GetUserId());
                    result = _brandServiceService.GetAll(_ => _.Service)
                        .Where(_ => _.BrandId == emp.BrandId).ToList();
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
        

        [HttpPost]
        [Route("")]
        [Authorize]
        public async Task<IHttpActionResult> Create(BrandServiceCreateVM viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                if (User.IsInRole(AccountType.ADMINISTRATOR.ToString()))
                {
                    return Unauthorized();
                }
                else
                {
                    var emp = await _accountService.GetEmployeeByAccount(User.Identity.GetUserId());
                    var model = ModelMapper.ConvertToModel(viewModel);
                    model.BrandId = emp.BrandId ?? 1;
                    _brandServiceService.Create(model);
                }


                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPut, Route("")]
        public IHttpActionResult Update(BrandServiceUpdateVM viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var brandService = _brandServiceService.Get(element => element.Id == viewModel.Id);
                if (brandService == null) return BadRequest("Brand service not found");

                brandService.Price = viewModel.Price;
                brandService.Description = viewModel.Description;

                _brandServiceService.Update(brandService);

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


                var entity = _brandServiceService.Get(_ => _.Id == id);
                if (entity == null) return BadRequest("Not found!");

                _brandServiceService.Delete(entity);

                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
