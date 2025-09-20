using RiskExposureTracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RiskExposureTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        
        public AuthenticationController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserModel requestUser)
        {
            // Added null check ...................................
            if (requestUser == null || string.IsNullOrEmpty(requestUser.UserName) || string.IsNullOrEmpty(requestUser.Password))
            {
                return BadRequest("Username and password are required.");
            }

            // Find user by email ....................................
            var user = await _userManager.FindByEmailAsync(requestUser.UserName);
            if (user == null)
            {
                return BadRequest("Invalid username or password");
            }

            // Check password....................................
            var passwordValid = await _userManager.CheckPasswordAsync(user, requestUser.Password);
            if (!passwordValid)
            {
                return BadRequest("Invalid username or password");
            }

            //Get roles..........................................
            var roles = await _userManager.GetRolesAsync(user);
            if (roles == null || roles.Count == 0)
            {
                return BadRequest("User has no roles assigned.");
            }

            //Generate JWT token..................................
            var token = GenerateJSONWebToken(user, roles);

            return Ok(new { token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserModel requestUser)
        {
            // Validate input (null checking.......)..........................
            if (requestUser == null || string.IsNullOrEmpty(requestUser.UserName) || string.IsNullOrEmpty(requestUser.Password))
            {
                return BadRequest("Username and password are required.");
            }

            // Check if user already exists...........................
            var existingUser = await _userManager.FindByEmailAsync(requestUser.UserName);
            if (existingUser != null)
            {
                return BadRequest("User with this email already exists.");
            }

            // Create new user..........................................
            var newUser = new IdentityUser
            {
                UserName = requestUser.UserName,
                Email = requestUser.UserName, // assuming username is email
            };

            var createResult = await _userManager.CreateAsync(newUser, requestUser.Password);
            if (!createResult.Succeeded)
            {
                return BadRequest("User registration failed.");
            }

            // Assign role ............................
            var roleResult = await _userManager.AddToRoleAsync(newUser, "User");
            if (!roleResult.Succeeded)
            {
                return BadRequest("Role assignment failed.");
            }

            // Return success..........................................
            return Ok(new { Message = "User registered successfully." });
        }


        private string GenerateJSONWebToken(IdentityUser user, IList<string> roles)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: authClaims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
