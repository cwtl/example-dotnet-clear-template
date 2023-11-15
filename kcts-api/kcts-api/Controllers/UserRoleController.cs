using Application.Features.Auth.Commands;
using Application.Features.Auth.ResponseModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace kcts_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class UserRoleController : BaseController
    {
        public UserRoleController(IMediator mediator) : base(mediator)
        {
        }

        // POST: api/UserRole
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<UserResponse>> PostUserRole([FromBody] SetUserRoleCommand command)
        {
            var success = await _mediator.Send(command);
            return Ok(success);
        }
    }
}
