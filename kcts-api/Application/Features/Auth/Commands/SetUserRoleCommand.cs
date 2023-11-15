using Application.Auth.Models;
using Application.ExceptionMiddleware;
using Application.ExceptionMiddleware.Exceptions;
using Application.Features.Auth.ResponseModels;
using Application.Features.Common.ResponseModels;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace Application.Features.Auth.Commands
{
    public class SetUserRoleCommand : IRequest<UserResponse>
    {
        public required string UserId { get; set; }
        public required IList<string> Roles { get; set; } = new List<string>();
    }

    public class SetUserRoleCommandHandler : IRequestHandler<SetUserRoleCommand, UserResponse>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public SetUserRoleCommandHandler(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<UserResponse> Handle(SetUserRoleCommand command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(command.UserId);
            if (user == null)
                throw new EntityNotFoundException($"User with Id {command.UserId} not found.");

            var wrongRoles = command.Roles.Where(role => !_roleManager.RoleExistsAsync(role).Result).ToList();
            if (wrongRoles.Count > 0)
            {
                var resultResponse = new ResultResponse()
                {
                    Succeeded = false,
                    Errors = wrongRoles.Select(role => new ErrorInfo()
                    {
                        Code = nameof(CustomErrorCodes.ENTITY_NOT_FOUND),
                        Description = $"There is no such role as {role.ToUpper()}."
                    }).ToList()
                };
                throw new EntityNotFoundException(resultResponse);
            }

            command.Roles.Select(r => r.ToUpper());
            var existingRoles = _userManager.GetRolesAsync(user).Result.Select(role => role.ToUpper());
            var rolesToAdd = command.Roles.Where(role => !existingRoles.Contains(role)).ToList();
            var rolesToDelete = existingRoles.Where(role => !command.Roles.Contains(role)).ToList();
            await _userManager.RemoveFromRolesAsync(user, rolesToDelete);
            await _userManager.AddToRolesAsync(user, rolesToAdd);

            return _mapper.Map<UserResponse>(new Tuple<ApplicationUser, IList<string>>(user, _userManager.GetRolesAsync(user).Result));
        }

    }
}
