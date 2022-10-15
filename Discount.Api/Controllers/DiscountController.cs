using Application.Features.Discounts.Commands;
using Application.Features.Discounts.Queries;
using Consul;

namespace Discounts.Api.Controllers;
public class DiscountController : BaseApiController
{
    private readonly IConsulClient consulClient;
    public DiscountController(IConsulClient consulClient) => this.consulClient = consulClient;
    /// <summary>
    /// Creates a New Discount.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Create(CreateDiscountCommand command)
    {
        var Discount = await Mediator.Send(command);
        return StatusCode(Discount.StatusCode, Discount.Data == null ? Discount.Message : Discount.Data);
    }
    /// <summary>
    /// Gets all Discount.
    /// </summary>
    /// <returns></returns>
    [HttpPost("getAll")]
    public async Task<IActionResult> GetAll(GetAllDiscountQuery query)
    {

        var Discounts = await Mediator.Send(query);
        return StatusCode(Discounts.StatusCode, Discounts.Data == null ? Discounts.Message : Discounts.Data);
    }

    /// <summary>
    /// Gets Discount by Id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var Discount = await Mediator.Send(new GetDiscountByIdQuery { Id = id });
        return StatusCode(Discount.StatusCode, Discount.Data == null ? Discount.Message : Discount.Data);

    }
    /// <summary>
    /// Deletes Discount  based on Id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var Discount = await Mediator.Send(new DeleteDiscountByIdCommand { Id = id });
        return StatusCode(Discount.StatusCode, Discount.Data == null ? Discount.Message : Discount.Data);
    }
    /// <summary>
    /// Updates the Discount  based on Id.   
    /// </summary>
    /// <param name="id"></param>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateDiscountCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }
        var Discount = await Mediator.Send(command);
        return StatusCode(Discount.StatusCode, Discount.Data == null ? Discount.Message : Discount.Data);
    }
}
