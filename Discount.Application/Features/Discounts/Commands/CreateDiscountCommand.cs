using Application.Dto;
using FluentValidation;

namespace Application.Features.Discounts.Commands;
public class CreateDiscountCommand : IRequest<DiscountModel>
{
    public string DiscountName { get; set; }
    public string Description { get; set; }
    public decimal Percentage { get; set; }

    public class CreateDiscountCommandHandler : IRequestHandler<CreateDiscountCommand, DiscountModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CreateDiscountCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DiscountModel> Handle(CreateDiscountCommand command, CancellationToken cancellationToken)
        {
            var Discount = _mapper.Map<Discount>(command);
            _context.Discounts.Add(Discount);
            await _context.SaveChangesAsync();
            return new DiscountModel
            {
                Data = _mapper.Map<DiscountDto>(Discount),
                StatusCode = 200,
                Message = "Data has been added"
            };
        }
    }


}
public class CreateDiscountCommandValidator : AbstractValidator<CreateDiscountCommand>
{
    public CreateDiscountCommandValidator()
    {
        RuleFor(x => x.DiscountName)
            .NotEmpty()
            .WithMessage("Name should be not empty!");
    }
}
