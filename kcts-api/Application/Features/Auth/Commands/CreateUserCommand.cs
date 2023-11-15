using Application.Auth.Models;
using Application.ExceptionMiddleware.Exceptions;
using Application.Features.Common.ResponseModels;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Auth.Commands
{
    public class CreateUserCommand : IRequest<ResultResponse>
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ResultResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public CreateUserCommandHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<ResultResponse> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var identityResult = await _userManager.CreateAsync(
                new ApplicationUser() { UserName = command.UserName, Email = command.Email },
                command.Password
            );
            var resultResponse = _mapper.Map<IdentityResult, ResultResponse>(identityResult);
            if (!identityResult.Succeeded)
                throw new ConflictedCommandException(resultResponse);
            return resultResponse;
        }
    }
}
