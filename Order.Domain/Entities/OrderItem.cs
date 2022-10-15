namespace Domain.Entities;

public class OrderItem : BaseEntity
{
    public int OrderId { get; set; }
    public Order Order { get; set; }
    public decimal Price { get; set; }
    public int ServiceId { get; set; }
}