using Application.Dto;
using Application.Features.Services.Commands;

namespace Application.Mapper;
public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        CreateMap<Service, CreateServiceCommand>();
        CreateMap<CreateServiceCommand, Service>();
        CreateMap<Service, ServiceDto>();
    }
}

