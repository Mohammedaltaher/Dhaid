using Application.Dto;
using Application.Dto.Common;

namespace Application.Features.Services.Queries;
public class GetAllServiceQuery : Pagination, IRequest<ServicesModel>
{

    public class GetAllServiceQueryHandler : IRequestHandler<GetAllServiceQuery, ServicesModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetAllServiceQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServicesModel> Handle(GetAllServiceQuery query, CancellationToken cancellationToken)
        {

            var ServiceList = await _context.Services
                    .OrderBy(o => o.Name)
                    .Skip((query.PageNumber - 1) * query.PageSize)
                    .Take(query.PageSize)
                    .ToListAsync(cancellationToken: cancellationToken);
            _mapper.Map<List<ServiceDto>>(ServiceList);
            return new ServicesModel
            {
                Data = _mapper.Map<List<ServiceDto>>(ServiceList),
                StatusCode = 200,
                Message = "Data found"
            };
        }

    }
}
