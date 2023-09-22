using HardCode.Dal.Entites;
using Microsoft.EntityFrameworkCore;

namespace HardCode.Dal;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions options) : base(options)
    {
        Database.Migrate();
    }

    public DbSet<ProductEntity> ProductEntities { get; set; }
    public DbSet<CategoryEntity> CategoryEntities { get; set; }
    public DbSet<PropertyEntity> PropertyEntities { get; set; }
    public DbSet<ValueEntity> ValueEntities { get; set; }
}