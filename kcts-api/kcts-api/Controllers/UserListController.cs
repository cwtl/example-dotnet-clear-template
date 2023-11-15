using Application.Features.Auth.Queries;
using Application.Features.Auth.ResponseModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace kcts_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserListController : BaseController
    {
        public UserListController(IMediator mediator) : base(mediator) { }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetUsers()
        {
            var users = await _mediator.Send(new GetUserListQuery());
            return Ok(users);
        }

    }
}
