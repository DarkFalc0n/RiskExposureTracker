using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RiskExposureTracker.Models;
using RiskExposureTracker.Services;

namespace RiskExposureTracker.Controllers
{
    [Route("api/orgs")]
    [ApiController]
    [Authorize]
    public class OrganizationsController : ControllerBase
    {
        private readonly IOrganizationService _service;
        private readonly UserManager<OrgModel> _userManager;

        public OrganizationsController(
            IOrganizationService service,
            UserManager<OrgModel> userManager
        )
        {
            _service = service;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<OrgModel>>> GetAll()
        {
            var users = _userManager.Users.ToList();
            return Ok(users);
        }

        [HttpGet("{orgId}")]
        public async Task<IActionResult> GetOrganization(string orgId)
        {
            if (!User.IsInRole("Admin"))
            {
                var userId = _userManager.GetUserId(User);
                if (string.IsNullOrEmpty(userId))
                {
                    return NotFound();
                }
                else if (userId != orgId)
                {
                    return Forbid();
                }
            }

            var orgUser = await _userManager.FindByIdAsync(orgId);
            if (orgUser == null)
                return NotFound();

            return Ok(orgUser);
        }

        [HttpPut("{orgId}")]
        public async Task<IActionResult> UpdateOrganization(string orgId, OrgModel updatedOrg)
        {
            if (!User.IsInRole("Admin"))
            {
                var userId = _userManager.GetUserId(User);
                if (string.IsNullOrEmpty(userId) || userId != orgId)
                    return Forbid();
            }

            var existing = await _userManager.FindByIdAsync(orgId);
            if (existing == null)
                return NotFound();

            existing.Name = updatedOrg.Name;
            existing.Sector = updatedOrg.Sector;
            existing.Region = updatedOrg.Region;
            existing.Contact = updatedOrg.Contact;
            existing.Email = updatedOrg.Email;

            var result = await _userManager.UpdateAsync(existing);
            if (!result.Succeeded)
            {
                return ValidationProblem(
                    new ValidationProblemDetails(
                        result
                            .Errors.GroupBy(e => e.Code)
                            .ToDictionary(g => g.Key, g => g.Select(e => e.Description).ToArray())
                    )
                );
            }

            return Ok(new { Status = "Requested organization details are updated." });
        }
    }
}
