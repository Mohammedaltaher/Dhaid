using Application.Features.Orders.Commands;
using Application.Features.Orders.Queries;
using Consul;

namespace WebApi.Controllers;
public class OrderController : BaseApiController
{
    private readonly IConsulClient consulClient;
    public OrderController(IConsulClient consulClient) => this.consulClient = consulClient;
    /// <summary>
    /// Creates a New Order.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderCommand command)
    {
        var Order = await Mediator.Send(command);
        return StatusCode(Order.StatusCode, Order.Data == null ? Order.Message : Order.Data);
    }
    /// <summary>
    /// Gets all Order.
    /// </summary>
    /// <returns></returns>
    [HttpPost("getAll")]
    public async Task<IActionResult> GetAll(GetAllOrderQuery query)
    {

        var Orders = await Mediator.Send(query);
        return StatusCode(Orders.StatusCode, Orders.Data == null ? Orders.Message : Orders.Data);
    }

    /// <summary>
    /// Gets Order by Id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var Order = await Mediator.Send(new GetOrderByIdQuery { Id = id });
        return StatusCode(Order.StatusCode, Order.Data == null ? Order.Message : Order.Data);

    }
    /// <summary>
    /// Deletes Order  based on Id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var Order = await Mediator.Send(new DeleteOrderByIdCommand { Id = id });
        return StatusCode(Order.StatusCode, Order.Data == null ? Order.Message : Order.Data);
    }
    /// <summary>
    /// Updates the Order  based on Id.   
    /// </summary>
    /// <param name="id"></param>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateOrderCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }
        var Order = await Mediator.Send(command);
        return StatusCode(Order.StatusCode, Order.Data == null ? Order.Message : Order.Data);
    }
}
