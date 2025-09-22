using Microsoft.EntityFrameworkCore;
using RiskExposureTracker.Models;

namespace RiskExposureTracker.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly ApplicationDbContext _context;

        public OrganizationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Organization>> GetAllOrganizationsAsync()
        {
            var orgs = await _context.Organizations.ToListAsync();
            return orgs;
        }

        public async Task<Organization> GetByIdAsync(long orgId)
        {
            return await _context.Organizations.FindAsync(orgId);
        }

        public async Task UpdateAsync(Organization organization)
        {
            _context.Organizations.Update(organization);
            await _context.SaveChangesAsync();
        }
    }
}
