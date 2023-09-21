using System;

namespace TechTask.Dal.Entites;

public class ProductEntity : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public CategoryEntity? CategoryEntity { get; set; }
    public Guid CategoryId { get; set; }
    public List<ValueEntity> ValueEntities { get; set; }
    public decimal Price { get; set; }
}