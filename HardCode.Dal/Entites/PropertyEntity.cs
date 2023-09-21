
namespace HardCode.Dal.Entites;

public class PropertyEntity : BaseEntity
{
    public string Name { get; set; }
    public CategoryEntity? CategoryEntity { get; set; }
    public string Type { get; set; }
    public Guid CategoryId { get; set; }
}