using Application.Dto;

namespace Application.Features.Discounts.Queries;
public class GetDiscountByIdQuery : IRequest<DiscountModel>
{
    public int Id { get; set; }
    public class GetDiscountByIdQueryHandler : IRequestHandler<GetDiscountByIdQuery, DiscountModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetDiscountByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        public Task<DiscountModel> Handle(GetDiscountByIdQuery query, CancellationToken cancellationToken)
        {
            var Discount = _context.Discounts.Where(a => a.Id == query.Id).AsNoTracking().FirstOrDefault();
            if (Discount == null)
            {
                return Task.FromResult(new DiscountModel
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "No data found"
                });
            }
            return Task.FromResult(new DiscountModel
            {
                Data = _mapper.Map<DiscountDto>(Discount),
                StatusCode = 200,
                Message = "Data found"
            });
        }
    }
}
