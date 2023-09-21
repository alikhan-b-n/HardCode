using Microsoft.EntityFrameworkCore;
using TechTask.Dal.Entites;

namespace TechTask.Dal;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<ProductEntity> ProductEntities { get; set; }
    public DbSet<CategoryEntity> CategoryEntities { get; set; }
    public DbSet<PropertyEntity> PropertyEntities { get; set; }
    public DbSet<ValueEntity> ValueEntities { get; set; }
}