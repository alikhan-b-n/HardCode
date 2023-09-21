using TechTask.Dal.AdditionalModels;
using TechTask.Dal.Entites;

namespace TechTask.Bll.Dtos;

public class ValueDto
{
    public string Value { get; set; }
    public TypeEnum Type { get; set; }
    public PropertyEntity PropertyEntity { get; set; }
    public Guid PropertyId { get; set; }
    public ProductDto ProductDto { get; set; }
    public Guid ProductId { get; set; }
}