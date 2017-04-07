using Microsoft.AspNet.Identity.EntityFramework;

namespace AuthService.Core
{
    public class UsersStore : UserStore<IdentityUser>
    {
        public UsersStore() : base(new UsersContext())
        {

        }
    }
}