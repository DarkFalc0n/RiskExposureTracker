using Microsoft.AspNetCore.Identity;
using RiskExposureTracker.Models;

namespace RiskExposureTracker.Services
{
    public interface IOrganizationService
    {
        Task<IEnumerable<OrgModel>> GetAllOrganizationsServicAsync();
        Task<OrgModel?> GetOrganizationAsync(string orgId);
        Task<IdentityResult> UpdateOrganizationAsync(OrgModel updatedOrg);
    }
}
