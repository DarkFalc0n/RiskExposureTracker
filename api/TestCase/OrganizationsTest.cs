using RiskExposureTracker.Controllers;
using RiskExposureTracker.Models;
using RiskExposureTracker.Services;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CapstoneProject.Tests.Controllers
{
    public class OrganizationsControllerTests
    {
        private readonly IOrganizationService _mockService;
        private readonly OrganizationsController _controller;

        public OrganizationsControllerTests()
        {
            _mockService = Substitute.For<IOrganizationService>();
            _controller = new OrganizationsController(_mockService);
        }

        [Fact]
        public async Task GetAll_WhenOrganizationsExist_ReturnsOkWithOrganizations()
        {
            // Arrange
            var organizations = new List<Organizations>
            {
                new Organizations { OrgId = 1, Name = "Org 1", Sector = "Tech", Region = "US" },
                new Organizations { OrgId = 2, Name = "Org 2", Sector = "Finance", Region = "EU" }
            };
            _mockService.GetAllOrganizationsServicAsync().Returns(organizations);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<Organizations>>(okResult.Value);
            Assert.Equal(2, returnValue.Count());
            await _mockService.Received().GetAllOrganizationsServicAsync();
        }

        [Fact]
        public async Task GetOrganization_WhenOrgExists_ReturnsOkWithOrganization()
        {
            // Arrange
            var organization = new Organizations { OrgId = 1, Name = "Org 1", Sector = "Tech", Region = "US" };
            _mockService.GetOrganizationAsync(1).Returns(organization);

            // Act
            var result = await _controller.GetOrganization(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Organizations>(okResult.Value);
            Assert.Equal("Org 1", returnValue.Name);
            await _mockService.Received().GetOrganizationAsync(1);
        }

        [Fact]
        public async Task GetOrganization_WhenOrgDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            _mockService.GetOrganizationAsync(1).Returns((Organizations)null);

            // Act
            var result = await _controller.GetOrganization(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
            await _mockService.Received().GetOrganizationAsync(1);
        }

        [Fact]
        public async Task UpdateOrganization_WhenOrgIdMismatch_ReturnsBadRequest()
        {
            // Arrange
            var updatedOrg = new Organizations { OrgId = 2, Name = "Mismatch Org" };

            // Act
            var result = await _controller.UpdateOrganization(1, updatedOrg);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid Organization Id", badRequestResult.Value);
        }

        [Fact]
        public async Task UpdateOrganization_WhenOrgNotFound_ReturnsNotFound()
        {
            // Arrange
            var updatedOrg = new Organizations { OrgId = 1, Name = "Updated Org" };
            _mockService.GetOrganizationAsync(1).Returns((Organizations)null);

            // Act
            var result = await _controller.UpdateOrganization(1, updatedOrg);

            // Assert
            Assert.IsType<NotFoundResult>(result);
            await _mockService.Received().GetOrganizationAsync(1);
        }

        [Fact]
        public async Task UpdateOrganization_WhenValid_ReturnsOkWithStatus()
        {
            // Arrange
            var existingOrg = new Organizations
            {
                OrgId = 1,
                Name = "Org 1",
                Sector = "Tech",
                Region = "US",
                Contact = "123456789",
                Email = "org1@example.com"
            };

            var updatedOrg = new Organizations
            {
                OrgId = 1,
                Name = "Updated Org",
                Sector = "Finance",
                Region = "EU",
                Contact = "987654321",
                Email = "updated@example.com"
            };

            _mockService.GetOrganizationAsync(1).Returns(existingOrg);
            _mockService.UpdateOrganizationAsync(Arg.Any<Organizations>()).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateOrganization(1, updatedOrg);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            // Extract anonymous object property safely
            var statusProperty = okResult.Value?.GetType().GetProperty("Status")?.GetValue(okResult.Value, null)?.ToString();
            Assert.Equal("Requested organization details are updated.", statusProperty);

            await _mockService.Received().GetOrganizationAsync(1);
            await _mockService.Received().UpdateOrganizationAsync(Arg.Is<Organizations>(org =>
                org.Name == "Updated Org" &&
                org.Sector == "Finance" &&
                org.Region == "EU" &&
                org.Contact == "987654321" &&
                org.Email == "updated@example.com"
            ));
        }
    }
}
