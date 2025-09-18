using RiskExposureTracker.Models;

namespace RiskExposureTracker.Repositories
{
    public interface IRiskReportsRepository
    {
        Task<IEnumerable<RiskReport>> GetReportsByOrgAsync(long orgId);
        Task<IEnumerable<RiskReport>> GetReportsByOrgAndPeriodAsync(long orgId, string period);
        Task<RiskReport> AddReportAsync(RiskReport report);
    }
}
