using Application.Features.Services.Commands;
using Application.Features.Services.Queries;
using Consul;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace WebApi.Controllers;
public class ServiceController : BaseApiController
{
    private readonly IConsulClient _consulClient;
    public ServiceController(IConsulClient consulClient) => _consulClient = consulClient;
    /// <summary>
    /// Creates a New Service.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Create(CreateServiceCommand command)
    {
        var Service = await Mediator.Send(command);
        return StatusCode(Service.StatusCode, Service.Data == null ? Service.Message : Service.Data);
    }
  
    /// <summary>
    /// Gets all Service.
    /// </summary>
    /// <returns></returns>
    [HttpPost("getAll")]
    public async Task<IActionResult> GetAll(GetAllServiceQuery query)
    {

        var Services = await Mediator.Send(query);
        return StatusCode(Services.StatusCode, Services.Data == null ? Services.Message : Services.Data);
    }
    /// <summary>
    /// Gets Service by Id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var Service = await Mediator.Send(new GetServiceByIdQuery { Id = id });
        return StatusCode(Service.StatusCode, Service.Data == null ? Service.Message : Service.Data);

    }
    /// <summary>
    /// Deletes Service  based on Id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var Service = await Mediator.Send(new DeleteServiceByIdCommand { Id = id });
        return StatusCode(Service.StatusCode, Service.Data == null ? Service.Message : Service.Data);
    }
    /// <summary>
    /// Updates the Service  based on Id.   
    /// </summary>
    /// <param name="id"></param>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateServiceCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }
        var Service = await Mediator.Send(command);
        return StatusCode(Service.StatusCode, Service.Data == null ? Service.Message : Service.Data);
    }
}
