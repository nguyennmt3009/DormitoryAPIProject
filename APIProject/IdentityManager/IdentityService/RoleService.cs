namespace IdentityManager.IdentityService
{
    using IdentityManager.Entities;
    using Microsoft.AspNet.Identity;

    public class RoleService : RoleManager<Role>
    {
        public RoleService(IRoleStore<Role, string> store) : base(store)
        {

        }
    }
}
