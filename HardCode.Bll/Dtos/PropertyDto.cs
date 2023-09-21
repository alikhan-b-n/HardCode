namespace HardCode.Bll.Dtos;

public class PropertyDto
{
    public string Name { get; set; }
    public Guid CategoryId { get; set; }
    public CategoryDto CategoryDto { get; set; }
}