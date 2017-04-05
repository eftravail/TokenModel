using BooksAPI.Models;

namespace BooksAPI.ViewModels
{
    //NOTE: May seem like over-kill but mapping this to the Models.Review object ensures we only pass in to the service the elements we deem necessary.
    public class ReviewViewModel
    {
        public int BookID { get; set; }
        public int Rating { get; set; }
        public string Description { get; set; }

        public ReviewViewModel()
        {

        }

        public ReviewViewModel(Review review)
        {
            if (review == null)
            {
                return;
            }

            BookID = review.BookID;
            Rating = review.Rating;
            Description = review.Description;
        }



        public Review ToReview()
        {
            return new Review
            {
                BookID = BookID,
                Description = Description,
                Rating = Rating
            };
        }
    }
}