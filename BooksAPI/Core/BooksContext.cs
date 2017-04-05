using BooksAPI.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace BooksAPI.Core
{
    public class BooksContext : IdentityDbContext
    {
        public BooksContext()
            : base("name=BooksContext") //Allows Entity Framework to look up the connectionString item with this name
        {
            Configuration.ProxyCreationEnabled = false;
            Users.AsNoTracking().FirstOrDefaultAsync();
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}