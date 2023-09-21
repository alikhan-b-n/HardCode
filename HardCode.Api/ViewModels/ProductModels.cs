namespace TechTaskHardCode.ViewModels;

public class ProductParamModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public ProductCategoryViewModel ProductCategoryViewModel { get; set; }
}

public class ProductResponseModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public ProductCategoryViewModel ProductCategoryViewModel { get; set; }
    public Guid? Id { get; set; }
}


public class ProductCategoryViewModel
{
    public Guid CategoryId { get; set; }
    public List<ProductPropertyViewModel> Properties { get; set; }
}

public class ProductPropertyViewModel
{
    public string Name { get; set; }
    public string Type { get; set; }
    public string Value { get; set; }
}