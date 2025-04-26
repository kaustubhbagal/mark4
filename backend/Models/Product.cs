
namespace backend.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<FilledForm> FilledForms { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}

