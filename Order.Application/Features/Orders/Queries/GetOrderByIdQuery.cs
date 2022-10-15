using Application.Dto;

namespace Application.Features.Orders.Queries;
public class GetOrderByIdQuery : IRequest<OrderModel>
{
    public int Id { get; set; }
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetOrderByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        public Task<OrderModel> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
        {
            var Order = _context.Orders.Where(a => a.Id == query.Id).Include(x=>x.Items).AsNoTracking().FirstOrDefault();
            if (Order == null)
            {
                return Task.FromResult(new OrderModel
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "No data found"
                });
            }
            return Task.FromResult(new OrderModel
            {
                Data = _mapper.Map<OrderDto>(Order),
                StatusCode = 200,
                Message = "Data found"
            });
        }
    }
}
