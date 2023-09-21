namespace HardCode.Dal.Entites;

public class CategoryEntity : BaseEntity
{
    public string Name { get; set; }
    public List<ProductEntity> ProductEntities { get; set; }
}