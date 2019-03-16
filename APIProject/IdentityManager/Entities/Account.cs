namespace IdentityManager.Entities
{
    using Microsoft.AspNet.Identity.EntityFramework;

    public class Account : IdentityUser
    {
        public int? UserDormitoryId { get; set; }
    }
}
