using CapstoneProject_RiskExposureTrackingAndReportingSystem.Models;
using System.Security.Claims;

namespace CapstoneProject_RiskExposureTrackingAndReportingSystem.Repositories
{
    public interface IOrganizationRepository
    {
        Task<IEnumerable<Organizations>> GetAllOrganizationsAsync();
        Task<Organizations> GetByIdAsync(long orgId);
        Task UpdateAsync(Organizations organization);
    }
}
