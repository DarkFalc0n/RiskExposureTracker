using RiskExposureTracker.Models;

namespace RiskExposureTracker.Repositories
{
    public interface IMitigationsRepository
    {
        Task<bool> RiskExistsAsync(long riskId);
        Task<Mitigation> AddMitigationAsync(Mitigation mitigation);
        Task<IEnumerable<Mitigation>> GetMitigationsByRiskAsync(long riskId);
        Task<Mitigation?> GetByIdAsync(long id);
        Task<Mitigation?> UpdateMitigationAsync(Mitigation mitigation);
    }
}
