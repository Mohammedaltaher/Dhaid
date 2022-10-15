using Order.Application.Dto.Common;
using System.Net.Http;

namespace Order.Application.Services;
public interface IGatewayService
{
    Task<ServiceModel> GetService(int id);
    Task<DiscountModel> GetDiscount(int id);
}
public class GatewayService : IGatewayService
{
    private readonly HttpClient _client;
    public GatewayService(HttpClient client) => _client = client;

    public async Task<ServiceModel> GetService(int id) => await (await _client.GetAsync($"/gateway/services/service/{id}")).ReadContentAs<ServiceModel>();

    public async Task<DiscountModel> GetDiscount(int id) => await (await _client.GetAsync($"/gateway/discounts/discount/{id}")).ReadContentAs<DiscountModel>();

}
