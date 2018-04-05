using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SFMForFraudTransactions.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SFMForFraudTransactions.Controllers
{
    [Route("api/auth")]
    public class ApiAuthController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPasswordHasher<ApplicationUser> _hasher;
        private readonly IConfiguration _configuration;

        public ApiAuthController(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IPasswordHasher<ApplicationUser> hasher,
            IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _hasher = hasher;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] CredentialModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        return Ok();
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            return BadRequest();
        }

        [HttpPost("token")]
        public async Task<IActionResult> CreateToken([FromBody] CredentialModel model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    if (_hasher.VerifyHashedPassword(user, user.PasswordHash, model.Password) == PasswordVerificationResult.Success)
                    {
                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
                        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                            issuer: _configuration["Tokens:Issuer"],
                            audience: _configuration["Tokens:Audience"],
                            claims: claims,
                            expires: DateTime.UtcNow.AddMinutes(30),
                            signingCredentials: credentials
                            );

                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to generate token");
            }
            return BadRequest("Something Failed");
        }
    }
}
