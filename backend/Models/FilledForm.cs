
// namespace backend.Models
// {
//     public class FilledForm
//     {
//         public int Id { get; set; }

//         public int UserId { get; set; }
//         public int ProductId { get; set; }

//         public DateTime SubmittedAt { get; set; }

//         public string Q1 { get; set; }
//         public string Q2 { get; set; }
//         public string Q3 { get; set; }
//         public string Q4 { get; set; }
//         public string Q5 { get; set; }
//         public string Q6 { get; set; }
//         public string Q7 { get; set; }
//         public string Q8 { get; set; }
//         public string Q9 { get; set; }
//         public string Q10 { get; set; }

//         public User User { get; set; }
//         public Product Product { get; set; }
//     }

// }



using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class FilledForm
    {
        public int Id { get; set; }

        public int UserId { get; set; }  // Foreign Key for User
        [ForeignKey("UserId")]
        public User User { get; set; }  // Navigation property for User

        public int ProductId { get; set; }  // Foreign Key for Product
        [ForeignKey("ProductId")]
        public Product Product { get; set; }  // Navigation property for Product

        // Form fields
        public string? Q1 { get; set; }
        public string? Q2 { get; set; }
        public string? Q3 { get; set; }
        public string? Q4 { get; set; }
        public string? Q5 { get; set; }
        public string? Q6 { get; set; }
        public string? Q7 { get; set; }
        public string? Q8 { get; set; }
        public string? Q9 { get; set; }
        public string? Q10 { get; set; }

  
        public string? Summary { get; set; }

        public DateTime SubmittedAt { get; set; }
    }
}
