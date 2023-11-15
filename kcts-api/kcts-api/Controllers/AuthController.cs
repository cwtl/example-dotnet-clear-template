using Application.Features.Auth.Commands;
using Application.Features.Auth.ResponseModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace kcts_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        public AuthController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        public async Task<ActionResult<AuthResponse>> AuthUser([FromBody] AuthenticateUserCommand command)
        {
            var authResponse = await _mediator.Send(command);
            return Ok(authResponse);
        }

    }
}
