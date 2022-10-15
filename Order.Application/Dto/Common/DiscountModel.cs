namespace Order.Application.Dto.Common;

public class DiscountModel
{
    public int Id { get; set; }
    public string DiscountName { get; set; }
    public string Description { get; set; }
    public decimal Percentage { get; set; }
}