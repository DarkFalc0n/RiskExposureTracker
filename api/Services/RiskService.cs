using RiskExposureTracker.Models;
using RiskExposureTracker.Repositories;
using Microsoft.EntityFrameworkCore;

namespace RiskExposureTracker.Services
{
    public class RiskService : IRiskService
    {
        private readonly IRiskRepository _repository;
        public RiskService(IRiskRepository repository) => _repository = repository;

        public async Task<Risk> AddRiskAsync(Risk risk)
        {
            return await _repository.AddRiskAsync(risk);
        }

        public async Task<List<Risk>> GetRisksByOrgAsync(long orgId)
        {
            return await _repository.GetRisksByOrgAsync(orgId);
        }

        public async Task<Risk?> UpdateRiskAsync(long id, Risk updatedRisk)
        {
            var existingRisk = await _repository.GetRiskByIdAsync(id);
            if (existingRisk == null) return null;

            existingRisk.Category = updatedRisk.Category;
            existingRisk.Description = updatedRisk.Description;
            existingRisk.Exposure = updatedRisk.Exposure;
            existingRisk.Status = updatedRisk.Status;

            return await _repository.UpdateRiskAsync(existingRisk);
        }
    }
}
