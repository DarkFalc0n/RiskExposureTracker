using RiskExposureTracker.Models;

namespace RiskExposureTracker.Services
{
    public interface IMitigationsService
    {
        Task<Mitigation> CreateMitigationAsync(Mitigation mitigation);
        Task<IEnumerable<Mitigation>> GetMitigationsByRiskAsync(long riskId);
        Task<IEnumerable<Mitigation>> GetMitigationsByOrgIdAsync(string orgId);
        Task<Mitigation?> UpdateMitigationAsync(long id, Mitigation updated);
    }
}
