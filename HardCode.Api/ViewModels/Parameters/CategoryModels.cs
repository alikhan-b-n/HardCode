using TechTask.Dal.AdditionalModels;

namespace TechTaskHardCode.ViewModels.Parameters;

public class CreateCategoryModel
{
    public string Name { get; set; }
    public List<Property> Properties { get; set; }
}

public class Property
{
    public string Name { get; set; }
    public TypeEnum Type { get; set; }
}
