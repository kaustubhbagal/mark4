// using Microsoft.EntityFrameworkCore;
// using backend.Models;


// namespace backend.Data 
// {
//     public class ApplicationDbContext : DbContext
//     {
//         public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
//             : base(options) { }

//         public DbSet<User> Users { get; set; }
//         public DbSet<Product> Products { get; set; }
//         public DbSet<FilledForm> FilledForms { get; set; }
//         public DbSet<Review> Reviews { get; set; }
//     }
// }


using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Data 
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<FilledForm> FilledForms { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User - Review (One-to-Many)
            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Product - Review (One-to-Many)
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // User - FilledForm (One-to-Many)
            modelBuilder.Entity<FilledForm>()
                .HasOne(f => f.User)
                .WithMany(u => u.FilledForms)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Product - FilledForm (One-to-Many)
            modelBuilder.Entity<FilledForm>()
                .HasOne(f => f.Product)
                .WithMany(p => p.FilledForms)
                .HasForeignKey(f => f.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // Additional relationship configurations if needed can go here
        }
    }
}
