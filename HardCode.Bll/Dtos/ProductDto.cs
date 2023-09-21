namespace TechTask.Bll.Dtos;

public class ProductDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public Guid CategoryId { get; set; }
    public CategoryDto CategoryDto { get; set; }
    public List<ValueDto> ValueDtos { get; set; }
    public decimal Price { get; set; }
}