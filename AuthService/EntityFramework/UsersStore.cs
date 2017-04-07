using Microsoft.AspNet.Identity.EntityFramework;

namespace AuthService.EntityFramework
{
    public class UsersStore : UserStore<IdentityUser>
    {
        public UsersStore() : base(new UsersContext())
        {

        }
    }
}