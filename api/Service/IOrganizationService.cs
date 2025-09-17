using CapstoneProject_RiskExposureTrackingAndReportingSystem.Models;

namespace CapstoneProject_RiskExposureTrackingAndReportingSystem.Services
{
    public interface IOrganizationService
    {
        Task<IEnumerable<Organizations>> GetAllOrganizationsServicAsync();
        Task<Organizations> GetOrganizationAsync(long orgId);
        Task UpdateOrganizationAsync(Organizations updatedOrg);
    }
}
