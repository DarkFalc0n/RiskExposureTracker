using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RiskExposureTracker.Models;

namespace RiskExposureTracker.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<OrgModel> _userManager;

        public OrganizationRepository(
            ApplicationDbContext context,
            UserManager<OrgModel> userManager
        )
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IEnumerable<OrgModel>> GetAllOrganizationsAsync()
        {
            // User store as source of truth
            return await _userManager.Users.ToListAsync();
        }

        public async Task<OrgModel?> GetByIdAsync(string orgId)
        {
            return await _userManager.FindByIdAsync(orgId);
        }

        public async Task<IdentityResult> UpdateAsync(OrgModel organization)
        {
            return await _userManager.UpdateAsync(organization);
        }
    }
}
