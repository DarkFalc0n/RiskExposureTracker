using RiskExposureTracker.Models;

namespace RiskExposureTracker.Repositories
{
    public interface IRiskRepository
    {
        Task<Risk> AddRiskAsync(Risk risk);
        Task<List<Risk>> GetAllRisksAsync();

        Task<Risk?> GetRiskByIdAsync(long id);
        Task<List<Risk>> GetRisksByOrgAsync(string orgId);
        Task<Risk?> UpdateRiskAsync(Risk risk);
    }
}
