using capstone1.Data;

using capstone1.Models;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

namespace capstone1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RiskReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RiskReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/reports/{orgId} ---------------------------------------------------------

        [HttpGet("{orgId}")]
        public async Task<ActionResult<IEnumerable<RiskReports>>> GetReportsByOrg(long orgId)
        {
            var reports = await _context.RiskReports
                                        .Where(r => r.OrgId == orgId)
                                        .OrderByDescending(r => r.CreatedAt)
                                        .ToListAsync();

            if (reports == null || !reports.Any())
            {
                return NotFound($"Sorry !! No reports found for Organization ID {orgId}.");
            }

            return Ok(reports);
        }

    }
}
