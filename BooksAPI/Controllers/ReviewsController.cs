using BooksAPI.Core;
using BooksAPI.Models;
using BooksAPI.ViewModels;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;


namespace BooksAPI.Controllers
{
    public class ReviewsController : ApiController
    {
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] ReviewViewModel review)  //Note this accepts only the ReviewViewModel object and in that we map to the Models.Review object
        {
            using (var context = new BooksContext())
            {
                var book = await context.Books.FirstOrDefaultAsync(b => b.ID == review.BookID);
                if (book == null)
                {
                    return NotFound();
                }

                var newReview = context.Reviews.Add(new Review
                {
                    BookID = book.ID,
                    Description = review.Description,
                    Rating = review.Rating
                });

                await context.SaveChangesAsync();
                return Ok(new ReviewViewModel(newReview));
            }
        }

        [Authorize]
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            using (var context = new BooksContext())
            {
                var review = await context.Reviews.FirstOrDefaultAsync(r => r.ID == id);
                if (review == null)
                {
                    return NotFound();
                }

                context.Reviews.Remove(review);
                await context.SaveChangesAsync();
            }
            return Ok();
        }
    }
}
