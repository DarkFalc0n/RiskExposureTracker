using CapstoneProject_RiskExposureTrackingAndReportingSystem.Models;
using CapstoneProject_RiskExposureTrackingAndReportingSystem.Repositories;

namespace CapstoneProject_RiskExposureTrackingAndReportingSystem.Services
{
    public class OrganizationService:IOrganizationService
    {
        private readonly IOrganizationRepository _repository;

        public OrganizationService(IOrganizationRepository repository)
        {
            _repository = repository;
        }


        public async Task<IEnumerable<Organizations>> GetAllOrganizationsServicAsync()
        {
            return await _repository.GetAllOrganizationsAsync();
        }
        public async Task<Organizations> GetOrganizationAsync(long orgId)
        {
            return await _repository.GetByIdAsync(orgId);
        }

        public async Task UpdateOrganizationAsync(Organizations updatedOrg)
        {
            await _repository.UpdateAsync(updatedOrg);
        }

    }
}
