using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using RiskManagement.Controllers.v1;
using RiskManagement.Models;
using RiskManagement.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RiskTesting
{
    public class RisksControllerTests
    {
        private readonly Mock<IRiskService> _mockService;
        private readonly RisksController _controller;

        public RisksControllerTests()
        {
            _mockService = new Mock<IRiskService>();
            _controller = new RisksController(_mockService.Object);
        }

        [Fact]
        public async Task AddRisk_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var risk = new Risk
            {
                RiskId = 1,
                OrgId = 101,
                Category = "Financial",
                Description = "Market risk",
                Exposure = 5000,
                Status = "Open"
            };

            _mockService.Setup(s => s.AddRiskAsync(It.IsAny<Risk>()))
                        .ReturnsAsync(risk);

            // Act
            var result = await _controller.AddRisk(risk);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<Risk>(createdResult.Value);
            Assert.Equal(1, returnValue.RiskId);
            Assert.Equal("Financial", returnValue.Category);
        }

        [Fact]
        public async Task GetRisksByOrg_ReturnsOkWithRisks()
        {
            // Arrange
            var risks = new List<Risk>
            {
                new Risk { RiskId = 1, OrgId = 101, Category = "Financial", Description = "Market", Exposure = 1000, Status = "Open" },
                new Risk { RiskId = 2, OrgId = 101, Category = "Operational", Description = "System failure", Exposure = 2000, Status = "Closed" }
            };

            _mockService.Setup(s => s.GetRisksByOrgAsync(101))
                        .ReturnsAsync(risks);

            // Act
            var result = await _controller.GetRisksByOrg(101);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<List<Risk>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetRiskById_ReturnsNotFound_WhenRiskDoesNotExist()
        {
            // Arrange
            _mockService.Setup(s => s.GetRiskByIdAsync(99))
                        .ReturnsAsync((Risk?)null);

            // Act
            var result = await _controller.GetRiskById(99);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Contains("not found", notFoundResult.Value!.ToString(), System.StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task GetRiskById_ReturnsOk_WhenRiskExists()
        {
            // Arrange
            var risk = new Risk { RiskId = 1, OrgId = 101, Category = "Financial", Description = "Test", Exposure = 1500, Status = "Open" };
            _mockService.Setup(s => s.GetRiskByIdAsync(1))
                        .ReturnsAsync(risk);

            // Act
            var result = await _controller.GetRiskById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Risk>(okResult.Value);
            Assert.Equal(1, returnValue.RiskId);
        }

        [Fact]
        public async Task UpdateRisk_ReturnsNotFound_WhenRiskDoesNotExist()
        {
            // Arrange
            var updatedRisk = new Risk { RiskId = 99, OrgId = 101, Category = "Ops", Description = "Update", Exposure = 3000, Status = "Closed" };

            _mockService.Setup(s => s.UpdateRiskAsync(99, updatedRisk))
                        .ReturnsAsync((Risk?)null);

            // Act
            var result = await _controller.UpdateRisk(99, updatedRisk);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Contains("not found", notFoundResult.Value!.ToString(), System.StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task UpdateRisk_ReturnsOk_WhenRiskExists()
        {
            // Arrange
            var updatedRisk = new Risk { RiskId = 1, OrgId = 101, Category = "Ops", Description = "Update", Exposure = 3000, Status = "Closed" };

            _mockService.Setup(s => s.UpdateRiskAsync(1, updatedRisk))
                        .ReturnsAsync(updatedRisk);

            // Act
            var result = await _controller.UpdateRisk(1, updatedRisk);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Risk>(okResult.Value);
            Assert.Equal("Ops", returnValue.Category);
            Assert.Equal("Closed", returnValue.Status);
        }
    }
}
