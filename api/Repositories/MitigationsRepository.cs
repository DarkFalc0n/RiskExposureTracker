using Microsoft.EntityFrameworkCore;
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
            return await _context.Mitigations.Where(m => m.RiskId == riskId).ToListAsync();
        }

        // Fetch mitigations across all risks for an orgId
        public async Task<IEnumerable<Mitigation>> GetMitigationsByOrgIdAsync(string orgId)
        {
            return await _context
                .Mitigations.Where(m =>
                    _context.Risks.Any(r => r.RiskId == m.RiskId && r.OrgId == orgId)
                )
                .ToListAsync();
        }

        public async Task<Mitigation?> GetByIdAsync(long id)
        {
            return await _context.Mitigations.FindAsync(id);
        }

        public async Task<Mitigation?> UpdateMitigationAsync(Mitigation mitigation)
        {
            _context.Mitigations.Update(mitigation);
            await _context.SaveChangesAsync();
            return mitigation;
        }
    }
}
