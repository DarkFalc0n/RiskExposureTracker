using RiskExposureTracker.Models;
using RiskExposureTracker.Repositories;

namespace RiskExposureTracker.Services
{
    public class MitigationsService : IMitigationsService
    {
        private readonly IMitigationsRepository _repository;

        public MitigationsService(IMitigationsRepository repository)
        {
            _repository = repository;
        }

        public async Task<Mitigation> CreateMitigationAsync(Mitigation mitigation)
        {
            var riskExists = await _repository.RiskExistsAsync(mitigation.RiskId);
            if (!riskExists)
            {
                throw new ArgumentException($"Risk with ID {mitigation.RiskId} does not exist.");
            }

            if (string.IsNullOrEmpty(mitigation.Status))
            {
                mitigation.Status = "Open";
            }

            return await _repository.AddMitigationAsync(mitigation);
        }

        public async Task<IEnumerable<Mitigation>> GetMitigationsByRiskAsync(long riskId)
        {
            return await _repository.GetMitigationsByRiskAsync(riskId);
        }

        public async Task<Mitigation?> UpdateMitigationAsync(long id, Mitigation updated)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
            {
                return null;
            }

            existing.Action = updated.Action;
            existing.Owner = updated.Owner;
            existing.Deadline = updated.Deadline;
            existing.Status = updated.Status;

            return await _repository.UpdateMitigationAsync(existing);
        }
    }
}
