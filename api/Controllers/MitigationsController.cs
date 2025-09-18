using Microsoft.AspNetCore.Mvc;
using RiskExposureTracker.Models;
using RiskExposureTracker.Services;

namespace RiskExposureTracker.Controllers
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

        [HttpPost]
        public async Task<ActionResult<Mitigation>> PostMitigation(Mitigation mitigation)
        {
            try
            {
                var created = await _service.CreateMitigationAsync(mitigation);
                return CreatedAtAction(
                    nameof(GetMitigationsByRisk),
                    new { riskId = created.RiskId },
                    created
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

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
