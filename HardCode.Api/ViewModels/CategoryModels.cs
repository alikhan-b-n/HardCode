
namespace TechTaskHardCode.ViewModels;

public class CategoryParamModel
{
    public string Name { get; set; }
    public List<CategoryPropertyViewModel> Properties { get; set; }
}

public class CategoryResponseModel
{
    public string Name { get; set; }
    public List<CategoryPropertyViewModel> Properties { get; set; }
    public Guid? Id { get; set; }
}

public class CategoryPropertyViewModel
{
    public string Name { get; set; }
    public string Type { get; set; }
}
