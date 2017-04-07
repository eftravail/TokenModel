using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace AuthService.Core
{
    public class UsersContext : IdentityDbContext
    {
        public UsersContext()
            : base("name=UsersContext") //Allows Entity Framework to look up the connectionString item with this name
        {
            Configuration.ProxyCreationEnabled = false;
            Users.AsNoTracking().FirstOrDefaultAsync();
        }
    }
}