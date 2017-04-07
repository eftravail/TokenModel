using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;


namespace BooksAPI.Controllers
{
    //[Authorize(Roles = "user")]
    public class BooksController : ApiController
    {
        [Authorize]
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            using (var context = new Core.BooksContext())
            {
                return Ok(await context.Books.Include(x => x.Reviews).ToListAsync());
            }
        }
    }
}
