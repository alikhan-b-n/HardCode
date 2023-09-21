using HardCode.Dal.AdditionalModels;

namespace TechTaskHardCode.ViewModels.Parameters;

public class CreateCategoryModel
{
    public string Name { get; set; }
    public List<PropertyCategory> Properties { get; set; }
}

public class PropertyCategory
{
    public string Name { get; set; }
    public TypeEnum Type { get; set; }
}
