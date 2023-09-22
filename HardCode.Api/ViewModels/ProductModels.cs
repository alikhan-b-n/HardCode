namespace TechTaskHardCode.ViewModels;

public class ProductParamModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public ProductCategoryParamModel ProductCategoryParamModel { get; set; }
}

public class ProductResponseModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public ProductCategoryResponseModel ProductCategoryParamModel { get; set; }
    public Guid? Id { get; set; }
}


public class ProductCategoryParamModel
{
    public Guid CategoryId { get; set; }
    public List<ProductPropertyParamModel> Properties { get; set; }
}

public class ProductCategoryResponseModel
{
    public Guid CategoryId { get; set; }
    public List<ProductPropertyResponseModel> Properties { get; set; }
}

public class ProductPropertyParamModel
{
    public Guid Id { get; set; }
    public string Value { get; set; }
}

public class ProductPropertyResponseModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }
}