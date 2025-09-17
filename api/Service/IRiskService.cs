using RiskManagement.Models;

namespace RiskManagement.Service
{
    public interface IRiskService
    {
        Task<Risk> AddRiskAsync(Risk risk);
        Task<List<Risk>> GetRisksByOrgAsync(long orgId);
        Task<Risk?> UpdateRiskAsync(long id, Risk updatedRisk);
    }
}
