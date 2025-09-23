using Microsoft.EntityFrameworkCore;
using RiskExposureTracker.Data;
using RiskExposureTracker.Models;

namespace RiskExposureTracker.Repositories
{
    public class RiskReportsRepository : IRiskReportsRepository
    {
        private readonly ApplicationDbContext _context; // for db contxt

        public RiskReportsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RiskReport>> GetReportsByOrgAsync(string orgId)
        {
            return await _context
                .RiskReports.Where(r => r.OrgId == orgId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }

        // ---------------------------------------------------------------------------------------------------------------

        public async Task<IEnumerable<RiskReport>> GetReportsByOrgAndPeriodAsync(
            string orgId,
            string period
        )
        {
            return await _context
                .RiskReports.Where(r => r.OrgId == orgId && r.Period == period)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }

        public async Task<RiskReport> AddReportAsync(RiskReport report)
        {
            _context.RiskReports.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }
    }
}
