using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using RiskManagement.Data;
using RiskManagement.Models;

namespace RiskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RisksController : ControllerBase
    {
        private readonly RiskDbContext _context;
        public RisksController(RiskDbContext context) => _context = context;

        [HttpPost]
        public async Task<IActionResult> AddRisk(Risk risk)
        {
            _context.Risks.Add(risk);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRisksByOrg), new { orgId = risk.OrgId }, risk);
        }

        [HttpGet("{orgId}")]
        public async Task<IActionResult> GetRisksByOrg(long orgId)
        {
            var risks = await _context.Risks.Where(r => r.OrgId == orgId).ToListAsync();
            return Ok(risks);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRisk(long id, Risk updatedRisk)
        {
            var risk = await _context.Risks.FindAsync(id);

            if (risk == null)
            {
                return NotFound(new { message = $"Risk with ID {id} not found." });
            }

            risk.Category = updatedRisk.Category;
            risk.Description = updatedRisk.Description;
            risk.Exposure = updatedRisk.Exposure;
            risk.Status = updatedRisk.Status;

            _context.Risks.Update(risk);
            await _context.SaveChangesAsync();

            return Ok(risk);
        }
    }
}

