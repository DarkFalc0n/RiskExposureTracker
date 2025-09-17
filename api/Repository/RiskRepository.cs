using Microsoft.EntityFrameworkCore;
using RiskManagement.Data;
using RiskManagement.Models;

namespace RiskManagement.Repository
{
    public class RiskRepository :  IRiskRepository
    {
        private readonly RiskDbContext _context;
        public RiskRepository(RiskDbContext context) => _context = context;

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
