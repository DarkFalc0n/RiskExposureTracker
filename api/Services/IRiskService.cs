using RiskExposureTracker.Models;

namespace RiskExposureTracker.Services
{
    public interface IRiskService
    {
        Task<Risk> AddRiskAsync(Risk risk);
        Task<List<Risk>> GetRisksByOrgAsync(long orgId);        
        Task<Risk?> GetRiskByIdAsync(long id);
        Task<Risk?> UpdateRiskAsync(long id, Risk updatedRisk);
    }
}
