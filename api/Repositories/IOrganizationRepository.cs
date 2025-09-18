using RiskExposureTracker.Models;
using System.Security.Claims;

namespace RiskExposureTracker.Repositories
{
    public interface IOrganizationRepository
    {
        Task<IEnumerable<Organization>> GetAllOrganizationsAsync();
        Task<Organization> GetByIdAsync(long orgId);
        Task UpdateAsync(Organization organization);
    }
}
