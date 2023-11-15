using Application.Auth.Models;
using Application.Features.Auth.ResponseModels;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Auth.Queries
{
    public class GetUserListQuery : IRequest<IEnumerable<UserResponse>>
    {
    }

    public class GetUsersQueryHandler : IRequestHandler<GetUserListQuery, IEnumerable<UserResponse>>
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        public GetUsersQueryHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public Task<IEnumerable<UserResponse>> Handle(GetUserListQuery command, CancellationToken cancellationToken)
        {
            var users = _userManager.Users.AsNoTracking()
                .Select(user => _mapper.Map<UserResponse>(new Tuple<ApplicationUser, IList<string>>(user, _userManager.GetRolesAsync(user).Result)));

            return Task.FromResult<IEnumerable<UserResponse>>(users);
        }

    }
}
