using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AuthService.EntityFramework
{
    public class UsersManager : UserManager<IdentityUser>
    {
        public UsersManager() : base(new UsersStore())
        {

        }
    }
}