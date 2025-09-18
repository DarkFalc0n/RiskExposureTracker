using RiskExposureTracker.Models;

namespace RiskExposureTracker.Services
{
    public interface IOrganizationService
    {
        Task<IEnumerable<Organization>> GetAllOrganizationsServicAsync();
        Task<Organization> GetOrganizationAsync(long orgId);
        Task UpdateOrganizationAsync(Organization updatedOrg);
    }
}
