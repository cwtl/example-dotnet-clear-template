using Application.Auth.Models;
using Application.Auth.Services;
using Application.ExceptionMiddleware;
using Application.ExceptionMiddleware.Exceptions;
using Application.Features.Common.ResponseModels;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Auth.Commands
{
    public class DeleteRoleCommand : IRequest<ResultResponse>
    {
        /// <summary>
        /// Role name. Case Insensitive.
        /// </summary>
        public required string RoleName { get; set; }
        /// <summary>
        /// Specifies if the role can be deleted even if there are users with this role. By default it is not allowed.
        /// </summary>
        public bool AllowToDeleteIfUsersExist { get; set; }
    }

    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, ResultResponse>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        public DeleteRoleCommandHandler(RoleManager<IdentityRole> roleManager,
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            IAuthService authService)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _userManager = userManager;
            _authService = authService;
        }
        public async Task<ResultResponse> Handle(DeleteRoleCommand command, CancellationToken cancellationToken)
        {
            if (!_authService.IsAuthenticated || !_authService.IsAdministrator)
                throw new NotAuthorizedCommandException($"The application user {_authService.CurrentUserName} is not authorized to perform the operation.");

            var role = await _roleManager.Roles.FirstOrDefaultAsync(t => t.NormalizedName == command.RoleName.ToUpper());
            if (role == null || string.IsNullOrEmpty(role.NormalizedName))
                throw new EntityNotFoundException($"There is no such role as {command.RoleName}.");
            var users = await _userManager.GetUsersInRoleAsync(role.NormalizedName);
            if (users != null && users.Count > 0 && !command.AllowToDeleteIfUsersExist)
                throw new ConflictedCommandException(CustomErrorCodes.ROLE_DELETE_NOT_ALLOWED_CONSTRAINT, $"There are users with role {command.RoleName.ToUpper()}");
            var result = await _roleManager.DeleteAsync(role);
            return _mapper.Map<IdentityResult, ResultResponse>(result);
        }
    }
}
