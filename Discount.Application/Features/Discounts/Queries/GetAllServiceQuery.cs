using Application.Dto;
using Application.Dto.Common;

namespace Application.Features.Discounts.Queries;
public class GetAllDiscountQuery : Pagination, IRequest<DiscountsModel>
{

    public class GetAllDiscountQueryHandler : IRequestHandler<GetAllDiscountQuery, DiscountsModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetAllDiscountQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DiscountsModel> Handle(GetAllDiscountQuery query, CancellationToken cancellationToken)
        {

            var DiscountList = await _context.Discounts
                    .OrderBy(o => o.DiscountName)
                    .Skip((query.PageNumber - 1) * query.PageSize)
                    .Take(query.PageSize)
                    .ToListAsync(cancellationToken: cancellationToken);
            _mapper.Map<List<DiscountDto>>(DiscountList);
            return new DiscountsModel
            {
                Data = _mapper.Map<List<DiscountDto>>(DiscountList),
                StatusCode = 200,
                Message = "Data found"
            };
        }

    }
}
