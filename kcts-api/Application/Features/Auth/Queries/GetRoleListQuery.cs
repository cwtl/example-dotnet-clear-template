using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Auth.Queries
{
    public class GetRoleListQuery : IRequest<IList<string>>
    {
    }

    public class GetRoleListQueryHandler : IRequestHandler<GetRoleListQuery, IList<string>>
    {
        private readonly IMapper _mapper;

        private readonly RoleManager<IdentityRole> _roleManager;
        public GetRoleListQueryHandler(RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }
        public async Task<IList<string>> Handle(GetRoleListQuery command, CancellationToken cancellationToken)
        {
            return await _roleManager.Roles.Select(role => role.NormalizedName ?? string.Empty).ToListAsync();
        }
    }
}
