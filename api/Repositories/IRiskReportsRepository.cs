using RiskExposureTracker.Models;

namespace RiskExposureTracker.Repositories
{
    public interface IRiskReportsRepository
    {
        Task<IEnumerable<RiskReport>> GetReportsByOrgAsync(string orgId);
        Task<IEnumerable<RiskReport>> GetReportsByOrgAndPeriodAsync(string orgId, string period);
        Task<RiskReport> AddReportAsync(RiskReport report);
    }
}
