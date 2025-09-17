using Microsoft.EntityFrameworkCore;
using Capstone1.Data;
using Capstone1.Models;


namespace Capstone1.Repositories
{
    public class RiskReportsRepository : IRiskReportsRepository
    {
        private readonly ApplicationDbContext _context; // for db contxt

        public RiskReportsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RiskReports>> GetReportsByOrgAsync(long orgId)
        {
            return await _context.RiskReports
                                 .Where(r => r.OrgId == orgId)
                                 .OrderByDescending(r => r.CreatedAt)
                                 .ToListAsync();
        }

        // ---------------------------------------------------------------------------------------------------------------

        public async Task<IEnumerable<RiskReports>> GetReportsByOrgAndPeriodAsync(long orgId, string period)
        {
            return await _context.RiskReports
                                 .Where(r => r.OrgId == orgId && r.Period == period)
                                 .OrderByDescending(r => r.CreatedAt)
                                 .ToListAsync();
        }

        public async Task<RiskReports> AddReportAsync(RiskReports report)
        {
            _context.RiskReports.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }
    }
}
