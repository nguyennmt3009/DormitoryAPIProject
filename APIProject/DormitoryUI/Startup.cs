using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using DormitoryUI.NinjectConfig;
using Microsoft.Owin;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;

[assembly: OwinStartup(typeof(DormitoryUI.Startup))]

namespace DormitoryUI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            WebApiConfig.Register(config);
            SwaggerConfig.Register(config);

            app.UseNinjectMiddleware(CreateKernel).UseNinjectWebApi(config);
        }

        private static StandardKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(new List<NinjectModule>
            {
                new BusinessConfig()
            });
            return kernel;
        }
    }
}
