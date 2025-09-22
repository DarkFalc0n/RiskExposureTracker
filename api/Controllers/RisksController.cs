using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RiskExposureTracker.Models;
using RiskExposureTracker.Services;

namespace RiskExposureTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RisksController : ControllerBase
    {
        private readonly IRiskService _service;

        public RisksController(IRiskService service) => _service = service;

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var risks = await _service.GetAllRisksAsync();
            return Ok(risks);
        }

        [HttpPost]
        public async Task<IActionResult> AddRisk(Risk risk)
        {
            var createdRisk = await _service.AddRiskAsync(risk);
            return CreatedAtAction(
                nameof(GetRisksByOrg),
                new { orgId = createdRisk.OrgId },
                createdRisk
            );
        }

        [HttpGet("{orgId}")]
        public async Task<IActionResult> GetRisksByOrg(string orgId)
        {
            var risks = await _service.GetRisksByOrgAsync(orgId);
            return Ok(risks);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRisk(long id, Risk updatedRisk)
        {
            var risk = await _service.UpdateRiskAsync(id, updatedRisk);
            if (risk == null)
                return NotFound(new { message = $"Risk with ID {id} not found." });
            return Ok(risk);
        }
    }
}
