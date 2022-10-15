using Application.Dto;
using Domain.Context;
using FluentValidation;

namespace Application.Features.Services.Commands;
public class CreateServiceCommand : IRequest<ServiceModel>
{
    public string Name { get; set; }
    public int Type { get; set; }
    public string Summary { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }

    public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, ServiceModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CreateServiceCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceModel> Handle(CreateServiceCommand command, CancellationToken cancellationToken)
        {
            var Service = _mapper.Map<Service>(command);
            _context.Services.Add(Service);
            await _context.SaveChangesAsync();
            return new ServiceModel
            {
                Data = _mapper.Map<ServiceDto>(Service),
                StatusCode = 200,
                Message = "Data has been added"
            };
        }
    }


}
public class CreateServiceCommandValidator : AbstractValidator<CreateServiceCommand>
{
    public CreateServiceCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name should be not empty!");
    }
}
