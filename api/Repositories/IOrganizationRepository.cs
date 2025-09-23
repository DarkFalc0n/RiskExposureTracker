using Microsoft.AspNetCore.Identity;
using RiskExposureTracker.Models;

namespace RiskExposureTracker.Repositories
{
    public interface IOrganizationRepository
    {
        Task<IEnumerable<OrgModel>> GetAllOrganizationsAsync();
        Task<OrgModel?> GetByIdAsync(string orgId);
        Task<IdentityResult> UpdateAsync(OrgModel organization);
    }
}
