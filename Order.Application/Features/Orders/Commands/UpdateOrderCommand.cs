using Application.Dto;

namespace Application.Features.Orders.Commands;
public class UpdateOrderCommand : IRequest<OrderModel>
{
    public int Id { get; set; }
    public string CustomerName { get; set; }
    public virtual List<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, OrderModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateOrderCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<OrderModel> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            var Order = _context.Orders.FirstOrDefault(a => a.Id == command.Id);

            if (Order == null)
            {
                return new OrderModel
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "no data found"
                };
            }
            else
            {
                Order.CustomerName = command.CustomerName;
                Order.Items = _mapper.Map<List<OrderItem>>(command.Items);

                await _context.SaveChangesAsync();
                return new OrderModel
                {
                    Data = _mapper.Map<OrderDto>(Order),
                    StatusCode = 200,
                    Message = "Data has been updated"
                };
            }
        }
    }
}
