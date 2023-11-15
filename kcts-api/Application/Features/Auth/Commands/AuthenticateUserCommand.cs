using Application.Auth.Models;
using Application.Auth.Services;
using Application.Features.Auth.ResponseModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.Features.Auth.Commands
{
    public class AuthenticateUserCommand : IRequest<AuthResponse>
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }

    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, AuthResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtTokenService _tokenService;
        private readonly ILogger<AuthenticateUserCommandHandler> _logger;
        public AuthenticateUserCommandHandler(UserManager<ApplicationUser> userManager, IJwtTokenService tokenService, ILogger<AuthenticateUserCommandHandler> logger)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _logger = logger;
        }

        public async Task<AuthResponse> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
                throw new UnauthorizedAccessException();

            var isPasswordOk = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!isPasswordOk)
                throw new UnauthorizedAccessException();

            _logger.LogInformation($"{user.UserName} with identity authenticated.");

            return await _tokenService.AuthenticateAsync(user);
        }
    }
}
