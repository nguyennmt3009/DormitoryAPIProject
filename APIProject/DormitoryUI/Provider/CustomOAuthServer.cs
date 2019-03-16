namespace DormitoryUI.Provider
{

    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.Owin.Security.OAuth;
    using IdentityManager.IdentityService;
    using IdentityManager.Entities;
    using DataAccess.Database;
    using Microsoft.Owin.Security;
    using System.Collections.Generic;

    public class CustomOAuthAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private readonly AccountAdapter iAccountService;

        public CustomOAuthAuthorizationServerProvider(IEntityContext context)
        {
            this.iAccountService = new AccountAdapter(context);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.Response.Headers.Add("Access-Control-Allow-Origin", new string[] { "*" });

            IdentityInfor infor = await this.iAccountService.Login(context.UserName, context.Password, context.Options.AuthenticationType);

            if (infor.IsError)
                context.SetError("invalid_grant", infor.Errors.FirstOrDefault());
            else
            {
                context.Validated(infor.Data as ClaimsIdentity);

            }
        }


    }

}