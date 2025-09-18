using RiskExposureTracker.Models;
using RiskExposureTracker.Repositories;

namespace RiskExposureTracker.Services
{
    public class OrganizationService:IOrganizationService
    {
        private readonly IOrganizationRepository _repository;

        public OrganizationService(IOrganizationRepository repository)
        {
            _repository = repository;
        }


        public async Task<IEnumerable<Organization>> GetAllOrganizationsServicAsync()
        {
            return await _repository.GetAllOrganizationsAsync();
        }
        public async Task<Organization> GetOrganizationAsync(long orgId)
        {
            return await _repository.GetByIdAsync(orgId);
        }

        public async Task UpdateOrganizationAsync(Organization updatedOrg)
        {
            await _repository.UpdateAsync(updatedOrg);
        }

    }
}
