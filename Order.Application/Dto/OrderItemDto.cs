namespace Application.Dto
{
    public class OrderItemDto
    {
        public int OrderId { get; set; }
        public decimal Price { get; set; }
        public int ServiceId { get; set; }
    }
}