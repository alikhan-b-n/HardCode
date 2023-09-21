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
    public List<Property> Properties { get; set; }
}
