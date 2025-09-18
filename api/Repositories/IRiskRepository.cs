using RiskExposureTracker.Models;

namespace RiskExposureTracker.Repositories
{
    public interface IRiskRepository
    {
        Task<Risk> AddRiskAsync(Risk risk);
        Task<List<Risk>> GetRisksByOrgAsync(long orgId);
        Task<Risk?> GetRiskByIdAsync(long id);
        Task<Risk?> UpdateRiskAsync(Risk risk);
    }
}
