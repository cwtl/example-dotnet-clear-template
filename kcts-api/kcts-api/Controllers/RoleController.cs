using Application.Features.Auth.Commands;
using Application.Features.Common.ResponseModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace kcts_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class RoleController : BaseController
    {
        public RoleController(IMediator mediator) : base(mediator) { }

        // POST: api/Role
        [HttpPost]
        public async Task<ActionResult<ResultResponse>> PostRole([FromBody] CreateRoleCommand command)
        {
            var success = await _mediator.Send(command);
            return Ok(success);
        }

        // DELETE: api/Role
        [HttpDelete]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<ResultResponse>> DeleteRole([FromBody] DeleteRoleCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

    }
}
