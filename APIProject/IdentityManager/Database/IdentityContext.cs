namespace IdentityManager.Database
{
    using IdentityManager.Entities;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class IdentityContext : IdentityDbContext<Account>
    {
        public IdentityContext() : base("Dormitory")
        {
        }
    }
}
