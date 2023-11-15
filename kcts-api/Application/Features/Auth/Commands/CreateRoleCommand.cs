using Application.ExceptionMiddleware.Exceptions;
using Application.Features.Common.ResponseModels;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Auth.Commands
{
    public class CreateRoleCommand : IRequest<ResultResponse>
    {
        public required string RoleName { get; set; }
    }

    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, ResultResponse>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        public CreateRoleCommandHandler(RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }
        public async Task<ResultResponse> Handle(CreateRoleCommand command, CancellationToken cancellationToken)
        {
            var identityResult = await _roleManager.CreateAsync(new IdentityRole(command.RoleName));
            var resultResponse = _mapper.Map<IdentityResult, ResultResponse>(identityResult);
            if (!resultResponse.Succeeded)
                throw new ConflictedCommandException(resultResponse);
            return resultResponse;
        }
    }
}
