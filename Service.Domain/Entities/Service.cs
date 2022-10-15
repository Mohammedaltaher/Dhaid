namespace Domain.Entities;
public class Service : BaseEntity
{
    public string Name { get; set; }
    public int Type { get; set; }
    public string Summary { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}
