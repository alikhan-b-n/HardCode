
namespace HardCode.Bll.Dtos;

public class ProductDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public ProductCategoryDto ProductCategoryDto { get; set; }
    public Guid? Id { get; set; }
}

public class ProductCategoryDto
{
    public Guid CategoryId { get; set; }
    public List<ProductPropertyDto> Properties { get; set; }
}

public class ProductPropertyDto
{
    public Guid Id { get; set; }
    public string Value { get; set; }
    
    public string Name { get; set; }
}



