using Application.Dto;

namespace Application.Features.Services.Commands;
public class DeleteServiceByIdCommand : IRequest<ServiceModel>
{
    public int Id { get; init; }
    public class DeleteServiceByIdCommandHandler : IRequestHandler<DeleteServiceByIdCommand, ServiceModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public DeleteServiceByIdCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceModel> Handle(DeleteServiceByIdCommand command, CancellationToken cancellationToken)
        {
            var Service = await _context.Services.Where(a => a.Id == command.Id).FirstOrDefaultAsync(cancellationToken: cancellationToken);
            if (Service == null)
            {
                return new ServiceModel
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "No data found"
                };
            }
            Service.IsDeleted = true;
            await _context.SaveChangesAsync();
            return new ServiceModel
            {
                Data = _mapper.Map<ServiceDto>(Service),
                StatusCode = 200,
                Message = "Data has been Deleted"
            };
        }
    }
}
