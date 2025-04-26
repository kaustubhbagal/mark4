namespace backend.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;  // Default value
        public string Email { get; set; } = string.Empty;     // Default value
        public string HashedPassword { get; set; } = string.Empty; // Default value

        public ICollection<FilledForm> FilledForms { get; set; } = new List<FilledForm>();  // Default to an empty list
        public ICollection<Review> Reviews { get; set; } = new List<Review>();  // Default to an empty list
    }
}
