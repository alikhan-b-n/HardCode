
namespace TechTaskHardCode.ViewModels;

public class CategoryParamModel
{
    public string Name { get; set; }
    public List<CategoryPropertyParamModel> Properties { get; set; }
}

public class CategoryResponseModel
{
    public string Name { get; set; }
    public List<CategoryPropertyResponseModel> Properties { get; set; }
    public Guid? Id { get; set; }
}

public class CategoryPropertyResponseModel
{
    public string Name { get; set; }
    public Guid PropertyId { get; set; }
}

public class CategoryPropertyParamModel
{
    public string Name { get; set; }
}

