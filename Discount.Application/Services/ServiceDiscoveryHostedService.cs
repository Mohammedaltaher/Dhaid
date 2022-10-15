using Consul;
using Microsoft.Extensions.Hosting;
using Application.Dto.Common;

namespace Application.Discounts;
public class DiscountDiscoveryHostedDiscount : IHostedService
{
    private readonly IConsulClient _client;
    private readonly ServiceConfig _config;
    private string _registrationId;
    public DiscountDiscoveryHostedDiscount(IConsulClient client, ServiceConfig config)
    {
        _client = client;
        _config = config;
    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _registrationId = $"{_config.ServiceName}-{_config.ServiceId}";
        var registration = new AgentServiceRegistration
        {
            ID = _registrationId,
            Name = _config.ServiceName,
            Address = new Uri(_config.ServiceAddress).Host,
            Port = new Uri(_config.ServiceAddress).Port
        };
        await _client.Agent.ServiceDeregister(registration.ID, cancellationToken);
        await _client.Agent.ServiceRegister(registration, cancellationToken);
    }
    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _client.Agent.ServiceDeregister(_registrationId, cancellationToken);
    }
}