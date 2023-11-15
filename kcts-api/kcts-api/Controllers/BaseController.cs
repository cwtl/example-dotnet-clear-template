using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace kcts_api.Controllers
{
    [Controller]
    public abstract class BaseController : ControllerBase
    {
        protected readonly IMediator _mediator;
        public BaseController(IMediator mediator) => _mediator = mediator;
    }
}
