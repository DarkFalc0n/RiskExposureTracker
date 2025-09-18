using Microsoft.EntityFrameworkCore;
using RiskExposureTracker.Data;
using RiskExposureTracker.Models;

namespace RiskExposureTracker.Repositories
{
    public class MitigationsRepository : IMitigationsRepository
    {
        private readonly ApplicationDbContext _context;

        public MitigationsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Check if related Risk exists
        public async Task<bool> RiskExistsAsync(long riskId)
        {
            return await _context.Risks.AnyAsync(r => r.RiskId == riskId);
        }

        // Insert mitigation record
        public async Task<Mitigation> AddMitigationAsync(Mitigation mitigation)
        {
            _context.Mitigations.Add(mitigation);
            await _context.SaveChangesAsync();
            return mitigation;
        }

        // Fetch mitigations by riskId
        public async Task<IEnumerable<Mitigation>> GetMitigationsByRiskAsync(long riskId)
        {
            return await _context.Mitigations
                                 .Where(m => m.RiskId == riskId)
                                 .ToListAsync();
        }
    }
}
