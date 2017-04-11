//using AuthService.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.DataHandler.Encoder;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Cryptography;

namespace AuthService.EntityFramework
{
    public class Configuration : DbMigrationsConfiguration<UsersContext>
    {
        /// <summary>
        /// Allows Entity Framework to generate database changes (add tables, stored procedures, etc.) automatically.
        /// </summary>
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(UsersContext context)
        {
#if DEBUG
            CreateUsers(context);
#endif
        }

        private void CreateUsers(UsersContext context)
        {
            string adminRoleId;
            string userRoleId;

            if (!context.Roles.Any())
            {
                adminRoleId = context.Roles.Add(new IdentityRole("Administrator")).Id;
                userRoleId = context.Roles.Add(new IdentityRole("User")).Id;
            }
            else
            {
                adminRoleId = context.Roles.First(c => c.Name == "Administrator").Id;
                userRoleId = context.Roles.First(c => c.Name == "User").Id;
            }

            context.SaveChanges();

            if (!context.Users.Any())
            {
                var userKey = new byte[32];
                RNGCryptoServiceProvider.Create().GetBytes(userKey);
                var userSecret = TextEncodings.Base64Url.Encode(userKey);

                var administrator = context.Users.Add(new IdentityUser("administrator") { Email = "admin@somesite.com", EmailConfirmed = true, SecurityStamp = userSecret });
                administrator.Roles.Add(new IdentityUserRole { RoleId = adminRoleId });

                userKey = new byte[32];
                RNGCryptoServiceProvider.Create().GetBytes(userKey);
                userSecret = TextEncodings.Base64Url.Encode(userKey);

                var standardUser = context.Users.Add(new IdentityUser("jonpreece") { Email = "jon@somesite.com", EmailConfirmed = true, SecurityStamp = userSecret });
                standardUser.Roles.Add(new IdentityUserRole { RoleId = userRoleId });

                context.SaveChanges();

                var store = new UsersStore();
                store.SetPasswordHashAsync(administrator, new UsersManager().PasswordHasher.HashPassword("administrator123"));
                store.SetPasswordHashAsync(standardUser, new UsersManager().PasswordHasher.HashPassword("user123"));
            }

            context.SaveChanges();
        }
    }
}