using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiskManagement.Data;
using RiskManagement.Models;
using RiskManagement.Service;

namespace RiskManagement.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/[controller]")]
    public class RisksController : ControllerBase
    {
        private readonly IRiskService _service;
        public RisksController(IRiskService service) => _service = service;

        [HttpPost]
        public async Task<IActionResult> AddRisk(Risk risk)
        {
            var createdRisk = await _service.AddRiskAsync(risk);
            return CreatedAtAction(nameof(GetRiskById), new { id = createdRisk.RiskId }, createdRisk);

        }

        [HttpGet("org/{orgId:long}")]
        public async Task<IActionResult> GetRisksByOrg(long orgId)
        {
            var risks = await _service.GetRisksByOrgAsync(orgId);
            return Ok(risks);
        }

        [HttpGet("riskid/{id:long}")]
        public async Task<IActionResult> GetRiskById(long id)
        {
            var risk = await _service.GetRiskByIdAsync(id);
            if (risk == null)
                return NotFound(new { message = $"Risk with ID {id} not found." });
            return Ok(risk);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRisk(long id, Risk updatedRisk)
        {
            var risk = await _service.UpdateRiskAsync(id, updatedRisk);
            if (risk == null) return NotFound(new { message = $"Risk with ID {id} not found." });
            return Ok(risk);
        }


    }
}
