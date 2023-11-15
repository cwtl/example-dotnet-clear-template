using Application.Auth.Enums;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? CurrentUserName
        {
            get
            {
                if (_httpContextAccessor.HttpContext.User.Identity == null)
                    throw new UnauthorizedAccessException("Identity is not defined.");
                return _httpContextAccessor.HttpContext.User.Identity.Name;
            }
        }

        public IEnumerable<Claim> CurrentUserRoles
        {
            get
            {
                return _httpContextAccessor.HttpContext.User.Claims.Where(t => t.Type == ClaimTypes.Role);
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return _httpContextAccessor.HttpContext.User.Identity != null
                    && _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
            }
        }

        public bool IsAdministrator
        {
            get
            {
                return _httpContextAccessor.HttpContext.User.Claims.Any(role => role.Value.ToUpper() == nameof(UserRoles.ADMINISTRATOR));
            }
        }
        public bool IsManager
        {
            get
            {
                return _httpContextAccessor.HttpContext.User.Claims.Any(role => role.Value.ToUpper() == nameof(UserRoles.MANAGER));
            }
        }
        public bool IsSupport
        {
            get
            {
                return _httpContextAccessor.HttpContext.User.Claims.Any(role => role.Value.ToUpper() == nameof(UserRoles.SUPPORT));
            }
        }
    }
}
