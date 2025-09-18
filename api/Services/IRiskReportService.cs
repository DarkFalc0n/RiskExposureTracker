using RiskExposureTracker.Models;

namespace RiskExposureTracker.Services
{
    public interface IRiskReportsService
    {
        Task<IEnumerable<RiskReport>> GetReportsByOrgAsync(long orgId);
        Task<IEnumerable<RiskReport>> GetReportsByOrgAndPeriodAsync(long orgId, string period);
        Task<RiskReport> CreateReportAsync(RiskReport report);
    }
}
