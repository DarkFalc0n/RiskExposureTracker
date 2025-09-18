// risk reports dependency add
/*builder.Services.AddScoped<IRiskReportsRepository, RiskReportsRepository>();
builder.Services.AddScoped<IRiskReportsService, RiskReportsService>();
*/

using Microsoft.AspNetCore.Mvc;
using RiskExposureTracker.Models;
using RiskExposureTracker.Services;

namespace RiskExposureTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RiskReportsController : ControllerBase
    {
        private readonly IRiskReportsService _service;

        public RiskReportsController(IRiskReportsService service)
        {
            _service = service;
        }

        [HttpGet("{orgId}")]
        public async Task<ActionResult<IEnumerable<RiskReport>>> GetReportsByOrg(long orgId)
        {
            var reports = await _service.GetReportsByOrgAsync(orgId);
            if (!reports.Any())
            {
                return NotFound($"No reports found for Organization ID {orgId}.");
            }
            return Ok(reports);
        }

        [HttpGet("{orgId}/{period}")]
        public async Task<ActionResult<IEnumerable<RiskReport>>> GetReportsByOrgAndPeriod(
            long orgId,
            string period
        )
        {
            var reports = await _service.GetReportsByOrgAndPeriodAsync(orgId, period);
            if (!reports.Any())
            {
                return NotFound($"No {period} reports found for Organization ID {orgId}.");
            }
            return Ok(reports);
        }

        [HttpPost]
        public async Task<ActionResult<RiskReport>> PostReport(RiskReport report)
        {
            var created = await _service.CreateReportAsync(report);
            return CreatedAtAction(nameof(GetReportsByOrg), new { orgId = created.OrgId }, created);
        }
    }
}
