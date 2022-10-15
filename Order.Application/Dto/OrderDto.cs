using Application.Dto.Common;

namespace Application.Dto;
public class OrderDto
{
    public int Id { get; set; }
    public string CustomerName { get; set; }
    public virtual List<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
    public decimal TotalPrice { get { return Items.Sum(x => x.Price); } }
    public decimal DiscountedAmount { get; set; }
    public decimal FinalPrice { get { return TotalPrice - DiscountedAmount; } }
}

public class OrderModel : BaseModel
{
    public OrderDto Data { get; set; }
}
public class OrdersModel : BaseModel
{
    public List<OrderDto> Data { get; set; }
}


