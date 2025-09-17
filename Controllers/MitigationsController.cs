// dependencies : in program.cs
/*builder.Services.AddScoped<IMitigationsRepository, MitigationsRepository>();
builder.Services.AddScoped<IMitigationsService, MitigationsService>();
*/


using capstone1.Models;
using capstone1.Services;
using Microsoft.AspNetCore.Mvc;

/*•	POST /api/mitigations — Log mitigation action.
•	GET /api/mitigations/{riskId} — Fetch actions per risk.
*/

namespace capstone1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MitigationsController : Controller
    {
        private readonly IMitigationsService _service; // service layer

        public MitigationsController(IMitigationsService service) // DI
        {
            _service = service;
        }

        // ---------------------------------------------------------------------------------

        // POST: api/mitigations
        [HttpPost]
        public async Task<ActionResult<Mitigation>> PostMitigation(Mitigation mitigation)
        {
            try
            {
                var created = await _service.CreateMitigationAsync(mitigation);
                return CreatedAtAction(nameof(GetMitigationsByRisk), new { riskId = created.RiskId }, created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // ---------------------------------------------------------------------------------

        // GET: api/mitigations/{riskId}
        [HttpGet("{riskId}")]
        public async Task<ActionResult<IEnumerable<Mitigation>>> GetMitigationsByRisk(long riskId)
        {
            var mitigations = await _service.GetMitigationsByRiskAsync(riskId);

            if (mitigations == null || !mitigations.Any())
            {
                return NotFound($"No mitigations found for Risk ID {riskId}.");
            }

            return Ok(mitigations);
        }
    }
}
