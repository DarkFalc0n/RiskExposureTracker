using capstone1.Data;
using capstone1.Models;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;



/*•	POST /api/mitigations — Log mitigation action.
•	GET /api/mitigations/{riskId} — Fetch actions per risk.
*/

namespace capstone1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MitigationsController : Controller
    {

        private readonly ApplicationDbContext _context; // db context

        public MitigationsController(ApplicationDbContext context) // DI
        {
            _context = context;
        }

        // ---------------------------------------------------------------------------------


        // POST: api/mitigations

        [HttpPost]
        
        public async Task<ActionResult<Mitigation>> PostMitigation(Mitigation mitigation)
        {
            // Check if related Risk exists
            var riskExists = await _context.Risks.AnyAsync(r => r.RiskId == mitigation.RiskId);
            if (!riskExists)
            {
                return BadRequest($"Risk with ID {mitigation.RiskId} does not exist.");
            }

            // Default status if not provided

            if (string.IsNullOrEmpty(mitigation.Status))
            {
                mitigation.Status = "Open";
            }

            _context.Mitigations.Add(mitigation);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMitigationsByRisk), new { riskId = mitigation.RiskId }, mitigation);
        }


        // ---------------------------------------------------------------------------------

        // GET: api/mitigations/{riskId}

        [HttpGet("{riskId}")]
        public async Task<ActionResult<IEnumerable<Mitigation>>> GetMitigationsByRisk(long riskId)
        {
            var mitigations = await _context.Mitigations
                                            .Where(m => m.RiskId == riskId)
                                            .ToListAsync();

            if (mitigations == null || !mitigations.Any())
            {
                return NotFound($"No mitigations found for Risk ID {riskId}.");
            }

            return Ok(mitigations);
        }
    }
}
