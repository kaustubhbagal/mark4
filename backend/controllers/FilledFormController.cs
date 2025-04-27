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

        [HttpPost]
        public async Task<ActionResult<FilledForm>> SubmitFilledForm(FilledForm filledForm)
        {
            if (filledForm == null)
                return BadRequest("FilledForm data is null.");

            if (filledForm.UserId == 0 || filledForm.ProductId == 0)
                return BadRequest("Required fields (UserId, ProductId) are missing or invalid.");

            var userExists = await _context.Users.AnyAsync(u => u.Id == filledForm.UserId);
            if (!userExists)
                return NotFound("User with the specified UserId not found.");

            var productExists = await _context.Products.AnyAsync(p => p.Id == filledForm.ProductId);
            if (!productExists)
                return NotFound("Product with the specified ProductId not found.");

            if (filledForm.SubmittedAt == default)
                filledForm.SubmittedAt = DateTime.UtcNow;

            try
            {
                _context.FilledForms.Add(filledForm);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetFilledForm), new { id = filledForm.Id }, filledForm);
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "Internal server error while saving the data.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FilledForm>> GetFilledForm(int id)
        {
            var filledForm = await _context.FilledForms.FindAsync(id);
            if (filledForm == null)
                return NotFound("Filled form not found.");

            return filledForm;
        }
    }
}
