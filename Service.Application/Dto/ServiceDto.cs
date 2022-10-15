using Application.Dto.Common;

namespace Application.Dto;
public class ServiceDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Type { get; set; }
    public string Summary { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}

public class ServiceModel : BaseModel
{
    public ServiceDto Data { get; set; }
}
public class ServicesModel : BaseModel
{
    public List<ServiceDto> Data { get; set; }
}


