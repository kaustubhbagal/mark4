// using Microsoft.AspNetCore.Mvc;
// using backend.Models;
// using backend.Data; // Assuming your DBContext is here
// using System.Linq;
// using System.Threading.Tasks;

// namespace backend.Controllers
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class JourneyController : ControllerBase
//     {
//         private readonly ApplicationDbContext _context;

//         public JourneyController(ApplicationDbContext context)
//         {
//             _context = context;
//         }

//         // Endpoint to save filled form data
//         [HttpPost("submit")]
//         public async Task<IActionResult> SubmitJourney([FromBody] FilledForm filledForm)
//         {
//             if (filledForm == null)
//             {
//                 return BadRequest("Invalid form data");
//             }

//             // Set the submission time and save the data
//             filledForm.SubmittedAt = DateTime.Now;

//             _context.FilledForms.Add(filledForm);
//             await _context.SaveChangesAsync();

//             return Ok(filledForm);
//         }

//         // Endpoint to retrieve the journey data for a specific product
//         [HttpGet("product/{productId}")]
//         public async Task<IActionResult> GetJourneyData(int productId)
//         {
//             var journeyData = await _context.FilledForms
//                 .Where(f => f.ProductId == productId)
//                 .ToListAsync();

//             return Ok(journeyData);
//         }
//     }
// }


using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.Data; // Assuming your DBContext is here
using Microsoft.EntityFrameworkCore; // Required for ToListAsync
using System.Linq;
using System.Threading.Tasks;
using System;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JourneyController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public JourneyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Endpoint to save filled form data
        [HttpPost("submit")]
        public async Task<IActionResult> SubmitJourney([FromBody] FilledForm filledForm)
        {
            if (filledForm == null)
            {
                return BadRequest("Invalid form data");
            }

            // Validate filled form fields (Ensure required fields are present)
            if (string.IsNullOrEmpty(filledForm.Q1) || string.IsNullOrEmpty(filledForm.Summary))
            {
                return BadRequest("Some required fields are missing.");
            }

            // Set the submission time and save the data
            filledForm.SubmittedAt = DateTime.Now;

            _context.FilledForms.Add(filledForm);
            await _context.SaveChangesAsync();

            return Ok(filledForm);
        }

        // Endpoint to retrieve the journey data for a specific product
        [HttpGet("product/{productId}")]
        public async Task<IActionResult> GetJourneyData(int productId)
        {
            var journeyData = await _context.FilledForms
                .Where(f => f.ProductId == productId)
                .ToListAsync();

            return Ok(journeyData);
        }
    }
}

