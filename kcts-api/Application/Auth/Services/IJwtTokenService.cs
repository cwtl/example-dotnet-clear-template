using Application.Auth.Models;
using Application.Features.Auth.ResponseModels;

namespace Application.Auth.Services
{
    public interface IJwtTokenService
    {
        public Task<AuthResponse> AuthenticateAsync(ApplicationUser user);
    }
}
