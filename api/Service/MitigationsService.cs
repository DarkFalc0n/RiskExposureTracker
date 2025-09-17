using capstone1.Models;
using capstone1.Repositories;

namespace capstone1.Services
{
    public class MitigationsService : IMitigationsService
    {
        private readonly IMitigationsRepository _repository;

        public MitigationsService(IMitigationsRepository repository)
        {
            _repository = repository;
        }

        // Create mitigation with business rules
        public async Task<Mitigation> CreateMitigationAsync(Mitigation mitigation)
        {
            // Check if Risk exists
            var riskExists = await _repository.RiskExistsAsync(mitigation.RiskId);
            if (!riskExists)
            {
                throw new ArgumentException($"Risk with ID {mitigation.RiskId} does not exist.");
            }

            // Default status if not provided
            if (string.IsNullOrEmpty(mitigation.Status))
            {
                mitigation.Status = "Open";
            }

            return await _repository.AddMitigationAsync(mitigation);
        }

        // Get all mitigations for a Risk
        public async Task<IEnumerable<Mitigation>> GetMitigationsByRiskAsync(long riskId)
        {
            return await _repository.GetMitigationsByRiskAsync(riskId);
        }
    }
}
