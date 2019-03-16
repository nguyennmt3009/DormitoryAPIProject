using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using DataAccess.Database;
using DormitoryUI.NinjectConfig;
using DormitoryUI.Provider;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
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

            #region Identity
            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/token"),
                AllowInsecureHttp = true,
                AccessTokenExpireTimeSpan = TimeSpan.FromSeconds(1800),
                Provider = new CustomOAuthAuthorizationServerProvider(Kernel.Get<IEntityContext>()),
            });
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            app.UseCors(CorsOptions.AllowAll);
            #endregion

            app.UseNinjectMiddleware(CreateKernel).UseNinjectWebApi(config);
        }

        private static StandardKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            return kernel;
        }

        private static StandardKernel _kernel;

        public static StandardKernel Kernel
        {
            get
            {
                if (_kernel == null)
                {
                    _kernel = new StandardKernel();
                    _kernel.Load(Assembly.GetExecutingAssembly());
                }
                return _kernel;
            }
        }
    }
}
