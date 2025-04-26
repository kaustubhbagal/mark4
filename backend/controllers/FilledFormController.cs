using Microsoft.AspNetCore.Mvc;
using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilledFormController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FilledFormController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST api/filledform
        [HttpPost]
        public async Task<ActionResult<FilledForm>> SubmitFilledForm(FilledForm filledForm)
        {
            if (filledForm == null)
            {
                return BadRequest("FilledForm data is null.");
            }

            // Validate required fields
            if (filledForm.UserId == 0 || filledForm.ProductId == 0)
            {
                return BadRequest("Required fields (UserId, ProductId) are missing or invalid.");
            }

            // Optional: Validate if the UserId and ProductId exist in the respective tables
            var userExists = await _context.Users.AnyAsync(u => u.Id == filledForm.UserId);
            if (!userExists)
            {
                return NotFound("User with the specified UserId not found.");
            }

            var productExists = await _context.Products.AnyAsync(p => p.Id == filledForm.ProductId);
            if (!productExists)
            {
                return NotFound("Product with the specified ProductId not found.");
            }

            // Ensure SubmittedAt is properly set
            if (filledForm.SubmittedAt == default)
            {
                filledForm.SubmittedAt = DateTime.UtcNow; // Set to current UTC time if not provided
            }

            try
            {
                // Add the filled form to the database
                _context.FilledForms.Add(filledForm);
                await _context.SaveChangesAsync();

                // Return the created filled form with a 201 status code
                return CreatedAtAction(nameof(GetFilledForm), new { id = filledForm.Id }, filledForm);
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "Internal server error while saving the data.");
            }
        }

        // GET api/filledform/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FilledForm>> GetFilledForm(int id)
        {
            var filledForm = await _context.FilledForms.FindAsync(id);

            if (filledForm == null)
            {
                return NotFound("Filled form not found.");
            }

            return filledForm;
        }
    }
}

protected override void Up(MigrationBuilder migrationBuilder)
{
    // Remove or comment out this block
    // migrationBuilder.CreateTable(
    //     name: "Products",
    //     columns: table => new
    //     {
    //         Id = table.Column<int>(nullable: false)
    //             .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
    //         Name = table.Column<string>(nullable: false),
    //         Description = table.Column<string>(nullable: false)
    //     },
    //     constraints: table =>
    //     {
    //         table.PrimaryKey("PK_Products", x => x.Id);
    //     });
}

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250425105243_filledforms', '7.0.0'); // Replace '7.0.0' with your EF Core version

DROP DATABASE CustomerExperienceDB;

CREATE DATABASE CustomerExperienceDB CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
