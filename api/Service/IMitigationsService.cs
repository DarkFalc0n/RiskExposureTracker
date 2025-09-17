using capstone1.Models;

namespace capstone1.Services
{
    public interface IMitigationsService
    {
        Task<Mitigation> CreateMitigationAsync(Mitigation mitigation);
        Task<IEnumerable<Mitigation>> GetMitigationsByRiskAsync(long riskId);
    }
}
