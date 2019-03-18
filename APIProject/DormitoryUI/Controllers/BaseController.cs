using DormitoryUI.ViewModels;
using System.Web.Http;

namespace DormitoryUI.Controllers
{
    public abstract class BaseController : ApiController
    {
        private _ModelMapping _modelMapper;

        public _ModelMapping ModelMapper => _modelMapper ?? (_modelMapper = new _ModelMapping());


    }
}
