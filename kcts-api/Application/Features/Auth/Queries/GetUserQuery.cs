using Application.Auth.Models;
using Application.ExceptionMiddleware.Exceptions;
using Application.Features.Auth.ResponseModels;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Auth.Queries
{
    public class GetUserQuery : IRequest<UserResponse>
    {
        public required string Id { get; set; }
    }

    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserResponse>
    {
        private readonly IMapper _mapper;

        private readonly UserManager<ApplicationUser> _userManager;
        public GetUserQueryHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<UserResponse> Handle(GetUserQuery command, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(users => users.Id == command.Id);
            if (user == null)
                throw new EntityNotFoundException($"User with Id {command.Id} not found.");

            var roles = await _userManager.GetRolesAsync(user);
            return _mapper.Map<UserResponse>(new Tuple<ApplicationUser, IList<string>>(user, roles));
        }
    }
}
