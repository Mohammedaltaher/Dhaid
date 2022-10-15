using Application.Dto;
using Domain.Context;

namespace Application.Features.Discounts.Commands;
public class DeleteDiscountByIdCommand : IRequest<DiscountModel>
{
    public int Id { get; init; }
    public class DeleteDiscountByIdCommandHandler : IRequestHandler<DeleteDiscountByIdCommand, DiscountModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public DeleteDiscountByIdCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DiscountModel> Handle(DeleteDiscountByIdCommand command, CancellationToken cancellationToken)
        {
            var Discount = await _context.Discounts.Where(a => a.Id == command.Id).FirstOrDefaultAsync(cancellationToken: cancellationToken);
            if (Discount == null)
            {
                return new DiscountModel
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "No data found"
                };
            }
            Discount.IsDeleted = true;
            await _context.SaveChangesAsync();
            return new DiscountModel
            {
                Data = _mapper.Map<DiscountDto>(Discount),
                StatusCode = 200,
                Message = "Data has been Deleted"
            };
        }
    }
}
