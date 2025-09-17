using CapstoneProject_RiskExposureTrackingAndReportingSystem.Models;
using CapstoneProject_RiskExposureTrackingAndReportingSystem.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CapstoneProject_RiskExposureTrackingAndReportingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        private readonly IOrganizationService _service;

        public OrganizationsController(IOrganizationService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Organizations>>> GetAll()
        {
            var orgs = await _service.GetAllOrganizationsServicAsync();
            return Ok(orgs);
        }


        [HttpGet("{orgId}")]
        public async Task<IActionResult> GetOrganization(long orgId)
        {
            var org = await _service.GetOrganizationAsync(orgId);
            if (org == null)
                return NotFound();

            return Ok(org);
        }

        [HttpPut("{orgId}")]
        public async Task<IActionResult> UpdateOrganization(long orgId, Organizations updatedOrg)
        {
            if (orgId != updatedOrg.OrgId)
                return BadRequest("Invalid Organization Id");

            var existingOrg = await _service.GetOrganizationAsync(orgId);
            if (existingOrg == null)
                return NotFound();

            
            existingOrg.Name = updatedOrg.Name;
            existingOrg.Sector = updatedOrg.Sector;
            existingOrg.Region = updatedOrg.Region;
            existingOrg.Contact = updatedOrg.Contact;
            existingOrg.Email = updatedOrg.Email;

            await _service.UpdateOrganizationAsync(existingOrg);

            return Ok(new { Status = "Requested organization details are updated." });
        }
    }
}
