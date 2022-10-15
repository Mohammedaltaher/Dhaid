using System.Linq;

namespace Domain.Entities;
public class Order : BaseEntity
{
    public string CustomerName { get; set; }
    public List<OrderItem> Items { get; set; }
    public int DiscountId { get; set; }
    public decimal TotalPrice { get { return Items.Sum(x => x.Price); } }
    public decimal DiscountedAmount { get; set; }
    public decimal FinalPrice { get { return TotalPrice - DiscountedAmount; } }
}
