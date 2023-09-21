using HardCode.Dal.AdditionalModels;

namespace TechTaskHardCode.ViewModels.Parameters;

public class CreateProductModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public Category Category { get; set; }
}

public class Category
{
    public Guid CategoryId { get; set; }
    public List<PropertyProduct> Properties { get; set; }
}

public class PropertyProduct
{
    public string Name { get; set; }
    public TypeEnum Type { get; set; }
    public string Value { get; set; }
}