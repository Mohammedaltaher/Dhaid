using Application.Dto;
using Application.Features.Orders.Commands;

namespace Application.Mapper;
public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<OrderItem, OrderItemDto>();
        CreateMap<OrderItemDto, OrderItem>();
        CreateMap<Domain.Entities.Order, CreateOrderCommand>()
             .ForMember(dest => dest.Items, dest => dest.MapFrom(source => source.Items));
        CreateMap<CreateOrderCommand, Domain.Entities.Order>();
        CreateMap<Domain.Entities.Order, OrderDto>();
    }
}

