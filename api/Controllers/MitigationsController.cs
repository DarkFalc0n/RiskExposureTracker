using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RiskExposureTracker.Models;
using RiskExposureTracker.Repositories;
using RiskExposureTracker.Services;

namespace RiskExposureTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MitigationsController : Controller
    {
        private readonly IMitigationsService _service;
        private readonly IRiskRepository _riskRepository;

        public MitigationsController(IMitigationsService service, IRiskRepository riskRepository)
        {
            _service = service;
            _riskRepository = riskRepository;
        }

        [HttpPost]
        public async Task<ActionResult<Mitigation>> PostMitigation(Mitigation mitigation)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Forbid();
            }

            var risk = await _riskRepository.GetRiskByIdAsync(mitigation.RiskId);
            if (risk == null)
            {
                return NotFound($"Risk with ID {mitigation.RiskId} not found.");
            }

            if (!string.Equals(risk.OrgId, userId, StringComparison.Ordinal))
            {
                return Forbid();
            }
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMitigation(long id, Mitigation updated)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Forbid();
            }

            if (updated.RiskId != 0)
            {
                var risk = await _riskRepository.GetRiskByIdAsync(updated.RiskId);
                if (risk == null)
                {
                    return NotFound(new { message = $"Risk with ID {updated.RiskId} not found." });
                }
                if (!string.Equals(risk.OrgId, userId, StringComparison.Ordinal))
                {
                    return Forbid();
                }
            }
            var result = await _service.UpdateMitigationAsync(id, updated);
            if (result == null)
            {
                return NotFound(new { message = $"Mitigation with ID {id} not found." });
            }
            return Ok(result);
        }
    }
}
