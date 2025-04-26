
namespace backend.Models
{
    public class Review
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public int ProductId { get; set; }

        public string Summary { get; set; }
        public int Rating { get; set; } // 1–5

        public User User { get; set; }
        public Product Product { get; set; }
    }

}
