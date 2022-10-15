namespace Domain.Entities;
public class Discount : BaseEntity
{
    public string DiscountName { get; set; }
    public string Description { get; set; }
    public decimal Percentage { get; set; }
}
