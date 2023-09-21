
namespace HardCode.Bll.Dtos;

public class CategoryDto
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public List<CategoryPropertyDto> Properties { get; set; }
}

public class CategoryPropertyDto
{
    public string Name { get; set; }
    public string Type { get; set; }
}