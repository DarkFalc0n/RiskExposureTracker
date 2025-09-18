using RiskExposureTracker.Models;
using RiskExposureTracker.Repositories;

namespace RiskExposureTracker.Services
{
    public class RiskReportsService : IRiskReportsService
    {
        private readonly IRiskReportsRepository _repository;

        public RiskReportsService(IRiskReportsRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<RiskReport>> GetReportsByOrgAsync(long orgId)
        {
            return await _repository.GetReportsByOrgAsync(orgId);
        }

        public async Task<IEnumerable<RiskReport>> GetReportsByOrgAndPeriodAsync(long orgId, string period)
        {
            return await _repository.GetReportsByOrgAndPeriodAsync(orgId, period);
        }

        public async Task<RiskReport> CreateReportAsync(RiskReport report)
        {
            if (report.CreatedAt == default)
            {
                report.CreatedAt = DateTime.UtcNow;
            }

            return await _repository.AddReportAsync(report);
        }
    }
}
