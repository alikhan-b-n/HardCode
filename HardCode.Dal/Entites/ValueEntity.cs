using TechTask.Dal.AdditionalModels;

namespace TechTask.Dal.Entites;

public class ValueEntity : BaseEntity
{
    public string Value { get; set; }
    public TypeEnum Type { get; set; }
    public PropertyEntity PropertyEntity { get; set; }
    public Guid PropertyId { get; set; }
    public ProductEntity ProductEntity { get; set; }
    public Guid ProductId { get; set; }
}