using Capstone1.Models;

namespace Capstone1.Services
{
    public interface IRiskReportsService
    {
        Task<IEnumerable<RiskReports>> GetReportsByOrgAsync(long orgId);
        Task<IEnumerable<RiskReports>> GetReportsByOrgAndPeriodAsync(long orgId, string period);
        Task<RiskReports> CreateReportAsync(RiskReports report);
    }
}
