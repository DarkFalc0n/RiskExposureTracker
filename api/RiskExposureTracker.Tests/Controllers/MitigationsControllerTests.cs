
using Microsoft.AspNetCore.Mvc;
using RiskExposureTracker.Controllers;
using RiskExposureTracker.Models;
using RiskExposureTracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace RiskExposureTracker.Tests.Controllers
{

    /* -----------------------------------------------------------------------------
    FAKE SERVICE: It acts as a stand-in for the real service to give us predictable test results.
    -----------------------------------------------------------------------------*/
    
    public class FakeMitigationsService : IMitigationsService
    {
        private readonly List<Mitigation> _mitigations;

        public FakeMitigationsService()
        {
            _mitigations = new List<Mitigation>
            {
                new Mitigation { MitigationId = 1, RiskId = 101, Action = "Action 1", Owner = "UserA", Deadline = DateTime.Now, Status = "Open" },
                new Mitigation { MitigationId = 2, RiskId = 101, Action = "Action 2", Owner = "UserB", Deadline = DateTime.Now, Status = "Completed" },
                new Mitigation { MitigationId = 3, RiskId = 102, Action = "Action 3", Owner = "UserC", Deadline = DateTime.Now, Status = "Open" }
            };
        }

        public Task<IEnumerable<Mitigation>> GetMitigationsByRiskAsync(long riskId)
        {
            // Simulate fetching data: return mitigations that match the riskId
            
            var results = _mitigations.Where(m => m.RiskId == riskId);
            return Task.FromResult(results);
        }

        public Task<Mitigation> CreateMitigationAsync(Mitigation mitigation)
        {
            // Simulate the business rule: if a riskId is 999, throw an exception.
            if (mitigation.RiskId == 999)
            {
                throw new ArgumentException("Risk with ID 999 does not exist.");
            }
            
            // Simulate a successful creation
            mitigation.MitigationId = _mitigations.Count + 1;
            return Task.FromResult(mitigation);
        }

        // We must implement all methods from the interface to satisfy the contract
        
        public Task<Mitigation> GetMitigationByIdAsync(long mitigationId)
        {
            var result = _mitigations.FirstOrDefault(m => m.MitigationId == mitigationId);
            
            return Task.FromResult(result);
        }

        public Task<IEnumerable<Mitigation>> GetAllMitigationsAsync()
        {
            return Task.FromResult(_mitigations.AsEnumerable());
        }
    }


    /*-----------------------------------------------------------------------------
    CONTROLLER TESTS: These are the unit tests for the MitigationsController
    -----------------------------------------------------------------------------*/
    
    public class MitigationsControllerTests
    {
        [Fact]
        public async Task GetMitigationsByRisk_WithExistingRiskId_ReturnsOkResultWithData()
        {
            // Arrange
            var fakeService = new FakeMitigationsService();
            var controller = new MitigationsController(fakeService);
            var existingRiskId = 101L;

            // Act
            var actionResult = await controller.GetMitigationsByRisk(existingRiskId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var mitigations = Assert.IsAssignableFrom<IEnumerable<Mitigation>>(okResult.Value);
            Assert.Equal(2, mitigations.Count()); // Our fake service has 2 mitigations for RiskId 101
        }

        [Theory]
        [InlineData(999L)] // A risk ID that does not exist in our fake data
        [InlineData(0L)]   // An invalid ID
        public async Task GetMitigationsByRisk_WithNonExistingRiskId_ReturnsNotFoundResult(long nonExistingRiskId)
        {
            // Arrange
            var fakeService = new FakeMitigationsService();
            var controller = new MitigationsController(fakeService);

            // Act
            var actionResult = await controller.GetMitigationsByRisk(nonExistingRiskId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task PostMitigation_WithValidData_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var fakeService = new FakeMitigationsService();
            var controller = new MitigationsController(fakeService);
            var newMitigation = new Mitigation { RiskId = 102, Action = "A new test action", Owner = "Tester" };

            // Act
            var actionResult = await controller.PostMitigation(newMitigation);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            Assert.Equal("GetMitigationsByRisk", createdAtActionResult.ActionName); // Checks it points to the correct method
            Assert.NotNull(createdAtActionResult.Value);
            Assert.IsType<Mitigation>(createdAtActionResult.Value);
        }

        [Fact]
        public async Task PostMitigation_WithNonExistingRiskId_ReturnsBadRequest()
        {
            // Arrange
            var fakeService = new FakeMitigationsService();
            var controller = new MitigationsController(fakeService);
            var newMitigation = new Mitigation
            {
                RiskId = 999,
                Action = "Action for a non-existent risk",
                Owner = "Tester",
                Deadline = DateTime.Now.AddDays(30)
            };

            // Act
            var actionResult = await controller.PostMitigation(newMitigation);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            Assert.Equal("Risk with ID 999 does not exist.", badRequestResult.Value);
        }
    }
}


