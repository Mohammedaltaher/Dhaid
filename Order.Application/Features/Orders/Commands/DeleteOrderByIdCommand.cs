using Application.Dto;
using Domain.Context;

namespace Application.Features.Orders.Commands;
public class DeleteOrderByIdCommand : IRequest<OrderModel>
{
    public int Id { get; init; }
    public class DeleteOrderByIdCommandHandler : IRequestHandler<DeleteOrderByIdCommand, OrderModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public DeleteOrderByIdCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<OrderModel> Handle(DeleteOrderByIdCommand command, CancellationToken cancellationToken)
        {
            var Order = await _context.Orders.Where(a => a.Id == command.Id).FirstOrDefaultAsync(cancellationToken: cancellationToken);
            if (Order == null)
            {
                return new OrderModel
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "No data found"
                };
            }
            Order.IsDeleted = true;
            await _context.SaveChangesAsync();
            return new OrderModel
            {
                Data = _mapper.Map<OrderDto>(Order),
                StatusCode = 200,
                Message = "Data has been Deleted"
            };
        }
    }
}
