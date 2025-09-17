using Capstone1.Models;

namespace Capstone1.Repositories
{
    public interface IRiskReportsRepository
    {
        Task<IEnumerable<RiskReports>> GetReportsByOrgAsync(long orgId);
        Task<IEnumerable<RiskReports>> GetReportsByOrgAndPeriodAsync(long orgId, string period);
        Task<RiskReports> AddReportAsync(RiskReports report);
    }
}
