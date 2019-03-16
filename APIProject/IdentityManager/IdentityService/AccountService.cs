namespace IdentityManager.IdentityService
{
    using IdentityManager.Entities;
    using Microsoft.AspNet.Identity;

    public class AccountService : UserManager<Account>
    {
        public AccountService(IUserStore<Account> store) : base(store)
        {

        }
    }
}
