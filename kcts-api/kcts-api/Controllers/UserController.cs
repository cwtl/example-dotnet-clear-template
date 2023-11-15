using Application.Features.Auth.Commands;
using Application.Features.Auth.Queries;
using Application.Features.Auth.ResponseModels;
using Application.Features.Common.ResponseModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace kcts_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : BaseController
    {
        public UserController(IMediator mediator) : base(mediator) { }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<UserResponse>> GetUser(GetUserQuery quiery)
        {
            var user = await _mediator.Send(quiery);
            return Ok(user);
        }

        // POST: api/User
        [HttpPost]
        public async Task<ActionResult> PostUser([FromBody] CreateUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // DELETE: api/User
        [HttpDelete]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<ResultResponse>> DeleteUser([FromBody] DeleteUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

    }
}
