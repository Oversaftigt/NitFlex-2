using IdentityMicroservice.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Register registerModel)
        {
            //Check if password and confirmpassword match
            if (registerModel.Password != registerModel.ConfirmPassword)
            {
                return BadRequest("Passwords must match");
            }

            //Try to create user
            var user = new IdentityUser { Email = registerModel.Email, UserName = registerModel.Email };
            var result = await _userManager.CreateAsync(user, registerModel.Password);

            if (result.Succeeded)
            {
                return Ok(new { message = "User has been registered successfully" });
            }
            return BadRequest(result.Errors);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login loginModel)
        {
            var user = await _userManager.FindByEmailAsync(loginModel.Email);
            if (user is not null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

                //Create jwtToken
                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    expires: DateTime.Now.AddMinutes(double.Parse(_configuration["Jwt:ExpiryMinutes"]!)),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)),
                    SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(token) });
            }

            return Unauthorized();
        }

        //[HttpPost("add-role")]
        //public async Task<IActionResult> AddRole([FromBody] string role)
        //{
        //    if (await _roleManager.RoleExistsAsync(role) is false)
        //    {
        //        var result = await _roleManager.CreateAsync(new IdentityRole(role));
        //        if (result.Succeeded)
        //        {
        //            return Ok(new { message = "Role added successfully" });
        //        }
        //        return BadRequest(result.Errors);
        //    }

        //    return BadRequest("Role already exists!");
        //}

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] UserRole userRole)
        {
            var user = await _userManager.FindByEmailAsync(userRole.Email);

            if (user is null)
            {
                return BadRequest("User not found");
            }

            var result = await _userManager.AddToRoleAsync(user, userRole.Role);

            if (result.Succeeded)
            {
                return Ok(new { message = user.Email + " has been assigned role " + userRole.Role });
            }

            return BadRequest(result.Errors);
        }

        [HttpGet("userId")]
        public async Task<IActionResult> GetUserInfo()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null)
            {
                return Unauthorized("User not logged in");
            }

            return Ok(userId);
        }

        [HttpGet("email")]
        public async Task<IActionResult> GetUserEmail()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (email is null)
            {
                return Unauthorized("Email not found");
            }

            return Ok(email);
        }
    }
}
