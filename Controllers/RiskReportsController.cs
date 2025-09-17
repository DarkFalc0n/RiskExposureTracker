// risk reports dependency add
/*builder.Services.AddScoped<IRiskReportsRepository, RiskReportsRepository>();
builder.Services.AddScoped<IRiskReportsService, RiskReportsService>();
*/

using Microsoft.AspNetCore.Mvc;
using capstone1.Models;
using capstone1.Services;

namespace capstone1.Controllers
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


        // GET: api/reports/{orgId} ----------------------------------------------------------------

        
        [HttpGet("{orgId}")]
        public async Task<ActionResult<IEnumerable<RiskReports>>> GetReportsByOrg(long orgId)
        {
            var reports = await _service.GetReportsByOrgAsync(orgId);
            if (!reports.Any())
            {
                return NotFound($"No reports found for Organization ID {orgId}.");
            }
            return Ok(reports);
        }



        // GET: api/reports/{orgId}/{period} --------------------------------------------------------

        
        [HttpGet("{orgId}/{period}")]
        public async Task<ActionResult<IEnumerable<RiskReports>>> GetReportsByOrgAndPeriod(long orgId, string period)
        {
            var reports = await _service.GetReportsByOrgAndPeriodAsync(orgId, period);
            if (!reports.Any())
            {
                return NotFound($"No {period} reports found for Organization ID {orgId}.");
            }
            return Ok(reports);
        }


        // POST: api/reports --------------------------------------------------------------------------
        
        [HttpPost]
        public async Task<ActionResult<RiskReports>> PostReport(RiskReports report)
        {
            var created = await _service.CreateReportAsync(report);
            return CreatedAtAction(nameof(GetReportsByOrg), new { orgId = created.OrgId }, created);
        }
    }
}
