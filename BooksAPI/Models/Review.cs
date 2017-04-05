namespace BooksAPI.Models
{
    public class Review
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public int BookID { get; set; }
    }
}