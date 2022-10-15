using Application.Dto;
using Application.Dto.Common;
using Order.Application.Services;

namespace Application.Features.Orders.Queries;
public class GetAllOrderQuery : Pagination, IRequest<OrdersModel>
{

    public class GetAllOrderQueryHandler : IRequestHandler<GetAllOrderQuery, OrdersModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IGatewayService _gatewayOrder;
        public GetAllOrderQueryHandler(IApplicationDbContext context, IMapper mapper , IGatewayService gatewayOrder)
        {
            _context = context;
            _mapper = mapper;
            _gatewayOrder = gatewayOrder;
        }
        public async Task<OrdersModel> Handle(GetAllOrderQuery query, CancellationToken cancellationToken)
        {
            var OrderList = await _context.Orders
                    .Include(x=>x.Items)
                    .OrderBy(o => o.CustomerName)
                    .Skip((query.PageNumber - 1) * query.PageSize)
                    .Take(query.PageSize)
                    .ToListAsync(cancellationToken: cancellationToken);
            _mapper.Map<List<OrderDto>>(OrderList);
            return new OrdersModel
            {
                Data = _mapper.Map<List<OrderDto>>(OrderList),
                StatusCode = 200,
                Message = "Data found"
            };
        }

    }
}
