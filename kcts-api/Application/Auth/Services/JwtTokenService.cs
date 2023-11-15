using Application.Auth.Models;
using Application.Features.Auth.ResponseModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Auth.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<JwtTokenService> _logger;

        public JwtTokenService(IConfiguration configuration, UserManager<ApplicationUser> userManager, ILogger<JwtTokenService> logger)
        {
            _configuration = configuration;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<AuthResponse> AuthenticateAsync(ApplicationUser user)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var secret = _configuration["JWT:Secret"];
            if (string.IsNullOrEmpty(secret))
                throw new Exception("Secret is not configured");
            var tokenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var userRoles = await _userManager.GetRolesAsync(user);
            if (user.UserName == null)
                throw new ArgumentNullException("UserName cannot be empty");
            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {

                Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, user.UserName) }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(tokenKey, SecurityAlgorithms.HmacSha256Signature),
                Claims = new Dictionary<string, object>(),
            };
            securityTokenDescriptor.Claims.Add("roles", userRoles.ToArray());
            securityTokenDescriptor.Claims.Add("userId", user.Id);
            securityTokenDescriptor.Claims.Add("userName", user.UserName);
            securityTokenDescriptor.Claims.Add("userEmail", user.Email);
            var token = tokenHandler.CreateToken(securityTokenDescriptor);
            _logger.LogInformation($"{user.UserName} has a new generated jwt token.");
            return new AuthResponse() { Token = tokenHandler.WriteToken(token) };
        }
    }
}
