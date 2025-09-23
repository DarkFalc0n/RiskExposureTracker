using RiskExposureTracker.Models;

namespace RiskExposureTracker.Services
{
    public interface IRiskReportsService
    {
        Task<IEnumerable<RiskReport>> GetReportsByOrgAsync(string orgId);
        Task<IEnumerable<RiskReport>> GetReportsByOrgAndPeriodAsync(string orgId, string period);
        Task<RiskReport> CreateReportAsync(RiskReport report);
    }
}
