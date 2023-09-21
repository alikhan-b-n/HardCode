using HardCode.Dal.AdditionalModels;

namespace HardCode.Bll.Dtos;

public class CategoryDto
{
    public string Name { get; set; }
    public List<Property> Properties { get; set; }
}

public class Property
{
    public string Name { get; set; }
    public TypeEnum Type { get; set; }
}