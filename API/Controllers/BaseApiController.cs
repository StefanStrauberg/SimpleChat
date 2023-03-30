using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        readonly IMediator _mediator;

        public BaseApiController()
            => _mediator = HttpContext.RequestServices.GetService<IMediator>();

        protected IMediator Mediator => _mediator;
    }
}