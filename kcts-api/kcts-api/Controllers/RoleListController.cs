using Application.Features.Auth.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace kcts_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class RoleListController : BaseController
    {
        public RoleListController(IMediator mediator) : base(mediator) { }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetRoles()
        {
            var users = await _mediator.Send(new GetRoleListQuery());
            return Ok(users);
        }

    }
}
