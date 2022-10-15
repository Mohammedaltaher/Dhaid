using Application.Dto;

namespace Application.Features.Discounts.Commands;
public class UpdateDiscountCommand : IRequest<DiscountModel>
{
    public int Id { get; set; }
    public string DiscountName { get; set; }
    public string Description { get; set; }
    public decimal Percentage { get; set; }
    public class UpdateDiscountCommandHandler : IRequestHandler<UpdateDiscountCommand, DiscountModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateDiscountCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DiscountModel> Handle(UpdateDiscountCommand command, CancellationToken cancellationToken)
        {
            var Discount = _context.Discounts.FirstOrDefault(a => a.Id == command.Id);

            if (Discount == null)
            {
                return new DiscountModel
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "no data found"
                };
            }
            else
            {
                Discount.DiscountName = command.DiscountName;
                Discount.Description = command.Description;
                Discount.Percentage = command.Percentage;

                await _context.SaveChangesAsync();
                return new DiscountModel
                {
                    Data = _mapper.Map<DiscountDto>(Discount),
                    StatusCode = 200,
                    Message = "Data has been updated"
                };
            }
        }
    }
}
