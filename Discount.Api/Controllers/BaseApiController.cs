using Microsoft.Extensions.DependencyInjection;

namespace Discounts.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseApiController : ControllerBase
{
    /// <summary>
    /// Get Mediator 
    /// </summary>
    public IMediator Mediator => HttpContext.RequestServices.GetService<IMediator>();
}
