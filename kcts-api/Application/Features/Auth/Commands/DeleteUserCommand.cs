using Application.Auth.Models;
using Application.Auth.Services;
using Application.ExceptionMiddleware.Exceptions;
using Application.Features.Common.ResponseModels;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Auth.Commands
{
    public class DeleteUserCommand : IRequest<ResultResponse>
    {
        public required string UserId { get; set; }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ResultResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        public DeleteUserCommandHandler(IMapper mapper, UserManager<ApplicationUser> userManager, IAuthService authService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _authService = authService;
        }
        public async Task<ResultResponse> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            if (!_authService.IsAuthenticated || !_authService.IsAdministrator)
                throw new NotAuthorizedCommandException($"The application user {_authService.CurrentUserName} is not authorized to perform the operation.");
            var user = await _userManager.FindByIdAsync(command.UserId);
            if (user == null)
                throw new EntityNotFoundException($"There is no user with Id {command.UserId}.");
            var result = await _userManager.DeleteAsync(user);
            return _mapper.Map<IdentityResult, ResultResponse>(result);
        }
    }
}
