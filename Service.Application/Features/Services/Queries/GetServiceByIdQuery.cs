using Application.Dto;

namespace Application.Features.Services.Queries;
public class GetServiceByIdQuery : IRequest<ServiceModel>
{
    public int Id { get; set; }
    public class GetServiceByIdQueryHandler : IRequestHandler<GetServiceByIdQuery, ServiceModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetServiceByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        public Task<ServiceModel> Handle(GetServiceByIdQuery query, CancellationToken cancellationToken)
        {
            var Service = _context.Services.Where(a => a.Id == query.Id).AsNoTracking().FirstOrDefault();
            if (Service == null)
            {
                return Task.FromResult(new ServiceModel
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "No data found"
                });
            }
            return Task.FromResult(new ServiceModel
            {
                Data = _mapper.Map<ServiceDto>(Service),
                StatusCode = 200,
                Message = "Data found"
            });
        }
    }
}
