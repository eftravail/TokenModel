using Microsoft.AspNet.Identity.EntityFramework;

namespace BooksAPI.Core
{
    public class BookUserStore : UserStore<IdentityUser>
    {
        public BookUserStore() : base(new BooksContext())
        {

        }
    }
}