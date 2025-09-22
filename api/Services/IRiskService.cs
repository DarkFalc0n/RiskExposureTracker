using RiskExposureTracker.Models;

namespace RiskExposureTracker.Services
{
    public interface IRiskService
    {
        Task<Risk> AddRiskAsync(Risk risk);
        Task<List<Risk>> GetAllRisksAsync();
        Task<List<Risk>> GetRisksByOrgAsync(string orgId);
        Task<Risk?> UpdateRiskAsync(long id, Risk updatedRisk);
    }
}
