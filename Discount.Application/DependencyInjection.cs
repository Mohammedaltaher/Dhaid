using Application.Filters;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Application.Mapper;
using Consul;
using Microsoft.Extensions.Hosting;
using Application.Discounts;
using Application.Dto.Common;

namespace Application;
public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());

        AssemblyScanner.FindValidatorsInAssembly(Assembly.GetExecutingAssembly())
          .ForEach(item => services.AddScoped(item.InterfaceType, item.ValidatorType));

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationPipelineBehaviorFilter<,>));

        MapperConfiguration mapperConfig = new(profile =>
        {
            profile.AddProfile(new DiscountProfile());
        });
        var mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);


        var serviceConfig = configuration.GetServiceConfig();
        services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
        {
            consulConfig.Address = new Uri(serviceConfig.ConsulClient);
        }, null, handlerOverride =>
        {
            handlerOverride.Proxy = null;
            handlerOverride.UseProxy = false;
        }));
        services.AddSingleton(serviceConfig);
        services.AddSingleton<IHostedService, DiscountDiscoveryHostedDiscount>();
    }
    public static ServiceConfig GetServiceConfig(this IConfiguration configuration) => new ServiceConfig
    {
        ServiceDiscoveryAddress = configuration["ServiceConfig:serviceDiscoveryAddress"],
        ServiceAddress = configuration["ServiceConfig:serviceAddress"],
        ServiceName = configuration["ServiceConfig:serviceName"],
        ServiceId = configuration["ServiceConfig:serviceId"],
        ConsulClient = configuration["ServiceConfig:consulClient"]
    };
}

