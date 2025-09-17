using CapstoneProject_RiskExposureTrackingAndReportingSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace CapstoneProject_RiskExposureTrackingAndReportingSystem.Repositories
{
    public class OrganizationRepository:IOrganizationRepository
    {
        private readonly ApplicationDbContext _context;

        public OrganizationRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Organizations>> GetAllOrganizationsAsync()
        {
            var orgs = await _context.Organizations.ToListAsync();
            return orgs;

        }

        public async Task<Organizations> GetByIdAsync(long orgId)
        {
            return await _context.Organizations.FindAsync(orgId);
        }

        public async Task UpdateAsync(Organizations organization)
        {
            _context.Organizations.Update(organization);
            await _context.SaveChangesAsync();
        }


    }
}
