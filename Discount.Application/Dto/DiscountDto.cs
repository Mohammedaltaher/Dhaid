using Application.Dto.Common;

namespace Application.Dto;
public class DiscountDto
{
    public int Id { get; set; }
    public string DiscountName { get; set; }
    public string Description { get; set; }
    public decimal Percentage { get; set; }
}

public class DiscountModel : BaseModel
{
    public DiscountDto Data { get; set; }
}
public class DiscountsModel : BaseModel
{
    public List<DiscountDto> Data { get; set; }
}


