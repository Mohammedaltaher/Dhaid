using Application.Dto;
using Application.Features.Discounts.Commands;

namespace Application.Mapper;
public class DiscountProfile : Profile
{
    public DiscountProfile()
    {
        CreateMap<Discount, CreateDiscountCommand>();
        CreateMap<CreateDiscountCommand, Discount>();
        CreateMap<Discount, DiscountDto>();
    }
}

