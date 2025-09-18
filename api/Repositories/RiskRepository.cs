using Microsoft.EntityFrameworkCore;
using RiskExposureTracker.Models;

namespace RiskExposureTracker.Repositories
{
    public class RiskRepository :  IRiskRepository
    {
        private readonly ApplicationDbContext _context;
        public RiskRepository(ApplicationDbContext context) => _context = context;

        public async Task<Risk> AddRiskAsync(Risk risk)
        {
            _context.Risks.Add(risk);
            await _context.SaveChangesAsync();
            return risk;
        }

        public async Task<List<Risk>> GetRisksByOrgAsync(long orgId)
        {
            return await _context.Risks
                                 .Where(r => r.OrgId == orgId)
                                 .ToListAsync();
        }

        public async Task<Risk?> GetRiskByIdAsync(long id)
        {
            return await _context.Risks.FindAsync(id);
        }
        public async Task<Risk?> UpdateRiskAsync(Risk risk)
        {
            _context.Risks.Update(risk);
            await _context.SaveChangesAsync();
            return risk;
        }
    }
}
