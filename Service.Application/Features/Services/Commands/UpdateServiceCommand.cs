using Application.Dto;

namespace Application.Features.Services.Commands;
public class UpdateServiceCommand : IRequest<ServiceModel>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Type { get; set; }
    public string Summary { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public class UpdateServiceCommandHandler : IRequestHandler<UpdateServiceCommand, ServiceModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateServiceCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceModel> Handle(UpdateServiceCommand command, CancellationToken cancellationToken)
        {
            var Service = _context.Services.FirstOrDefault(a => a.Id == command.Id);

            if (Service == null)
            {
                return new ServiceModel
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "no data found"
                };
            }
            else
            {
                Service.Name = command.Name;
                Service.Type = command.Type;
                Service.Description = command.Description;
                Service.Summary = command.Summary;
                Service.Price = command.Price;

                await _context.SaveChangesAsync();
                return new ServiceModel
                {
                    Data = _mapper.Map<ServiceDto>(Service),
                    StatusCode = 200,
                    Message = "Data has been updated"
                };
            }
        }
    }
}
