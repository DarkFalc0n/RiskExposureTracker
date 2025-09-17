using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiskManagement.Data;
using RiskManagement.Models;
using RiskManagement.Service;

namespace RiskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RisksController : ControllerBase
    {
        private readonly IRiskService _service;
        public RisksController(IRiskService service) => _service = service;

        // POST /api/risks
        [HttpPost]
        public async Task<IActionResult> AddRisk(Risk risk)
        {
            var createdRisk = await _service.AddRiskAsync(risk);
            return CreatedAtAction(nameof(GetRisksByOrg), new { orgId = createdRisk.OrgId }, createdRisk);
        }

        // GET /api/risks/{orgId}
        [HttpGet("{orgId}")]
        public async Task<IActionResult> GetRisksByOrg(long orgId)
        {
            var risks = await _service.GetRisksByOrgAsync(orgId);
            return Ok(risks);
        }

        // PUT /api/risks/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRisk(long id, Risk updatedRisk)
        {
            var risk = await _service.UpdateRiskAsync(id, updatedRisk);
            if (risk == null) return NotFound(new { message = $"Risk with ID {id} not found." });
            return Ok(risk);
        }
    }
}
