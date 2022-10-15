using Application.Dto;
using FluentValidation;
using Order.Application.Services;

namespace Application.Features.Orders.Commands;
public class CreateOrderCommand : IRequest<OrderModel>
{
    public string CustomerName { get; set; }
    public List<OrderItemDto> Items { get; set; }
    public int DiscountId { get; set; }
    public decimal DiscountedAmount { get; set; }

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IGatewayService _gatewayService;
        public CreateOrderCommandHandler(IApplicationDbContext context, IMapper mapper, IGatewayService gatewayService)
        {
            _context = context;
            _mapper = mapper;
            _gatewayService = gatewayService;
        }
        public async Task<OrderModel> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var pricedItems = new List<OrderItemDto>();
            foreach (var item in command.Items)
            {
                pricedItems.Add(new OrderItemDto
                {
                    ServiceId = item.ServiceId,
                    Price = (await _gatewayService.GetService(item.ServiceId)).Price,
                });
            }
            command.Items = pricedItems;
            command.DiscountedAmount = command.Items.Sum(x => x.Price) * (await _gatewayService.GetDiscount(command.DiscountId)).Percentage;
            var Order = _mapper.Map<Domain.Entities.Order>(command);

            _context.Orders.Add(Order);
            await _context.SaveChangesAsync();
            return new OrderModel
            {
                Data = _mapper.Map<OrderDto>(Order),
                StatusCode = 200,
                Message = "Data has been added"
            };
        }
    }


}
public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.CustomerName)
            .NotEmpty()
            .WithMessage("Name should be not empty!");
    }
}
