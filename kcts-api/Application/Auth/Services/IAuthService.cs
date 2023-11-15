using System.Security.Claims;

namespace Application.Auth.Services
{
    public interface IAuthService
    {
        public string? CurrentUserName { get; }
        public IEnumerable<Claim> CurrentUserRoles { get; }
        public bool IsAuthenticated { get; }
        public bool IsAdministrator { get; }
        public bool IsManager { get; }
        public bool IsSupport { get; }
    }
}
