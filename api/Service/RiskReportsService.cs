using Capstone1.Models;
using Capstone1.Repositories;

namespace Capstone1.Services
{
    public class RiskReportsService : IRiskReportsService
    {
        private readonly IRiskReportsRepository _repository;

        public RiskReportsService(IRiskReportsRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<RiskReports>> GetReportsByOrgAsync(long orgId)
        {
            return await _repository.GetReportsByOrgAsync(orgId);
        }

        public async Task<IEnumerable<RiskReports>> GetReportsByOrgAndPeriodAsync(long orgId, string period)
        {
            return await _repository.GetReportsByOrgAndPeriodAsync(orgId, period);
        }

        public async Task<RiskReports> CreateReportAsync(RiskReports report)
        {
            if (report.CreatedAt == default)
            {
                report.CreatedAt = DateTime.UtcNow;
            }

            return await _repository.AddReportAsync(report);
        }
    }
}
