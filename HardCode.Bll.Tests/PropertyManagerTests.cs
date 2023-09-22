using HardCode.Bll.Dtos;
using HardCode.Bll.Services;
using HardCode.Dal;
using HardCode.Dal.Entites;
using HardCode.Dal.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HardCode.Bll.Tests;

public class PropertyManagerTests
{
    private readonly ApplicationContext _applicationContext;
    private readonly Repository<PropertyEntity> _propertyRepository;
    private readonly Repository<CategoryEntity> _categoryRepository;

    public PropertyManagerTests()
    {
        var builder = new DbContextOptionsBuilder<ApplicationContext>();
        builder.UseInMemoryDatabase(GetType().Name);

        _applicationContext = new ApplicationContext(builder.Options);
        _applicationContext.Database.EnsureDeleted();
        _applicationContext.Database.EnsureCreated();

        _propertyRepository = new Repository<PropertyEntity>(_applicationContext);
        _categoryRepository = new Repository<CategoryEntity>(_applicationContext);
    }

    [Fact]
    public async Task CreateProperty_ShouldCreateProperty_PositiveTest()
    {
        // Arrange
        var propertyManager = new PropertyManager(_propertyRepository, _categoryRepository);
        var categoryEntity = new CategoryEntity
        {
            Name = "shoes"
        };
        // Adding category to context because of checking of relevant category in manager
        await _applicationContext.CategoryEntities.AddAsync(categoryEntity);
        await _applicationContext.SaveChangesAsync();
        var propertyDto = new PropertyDto
        {
            Name = "color",
            CategoryId = categoryEntity.Id
        };

        // Act
        await propertyManager.Create(propertyDto);

        // Assert
        var result = _applicationContext.PropertyEntities.First();
        Assert.Equal(propertyDto.Name, result.Name);
        Assert.Equal(propertyDto.CategoryId, result.CategoryId);
    }

    [Fact]
    public async Task DeleteProperty_ShouldDeleteProperty_PositiveTest()
    {
        // Arrange
        var propertyManager = new PropertyManager(_propertyRepository, _categoryRepository);
        var categoryEntity = new CategoryEntity
        {
            Name = "shoes"
        };
        var propertyEntity = new PropertyEntity
        {
            Name = "color",
            CategoryId = categoryEntity.Id
        };
        
        // Adding category to context because of checking of relevant category in manager
        await _applicationContext.PropertyEntities.AddAsync(propertyEntity);
        await _applicationContext.CategoryEntities.AddAsync(categoryEntity);
        await _applicationContext.SaveChangesAsync();
        // Act
        await propertyManager.Delete(propertyEntity.Id);
        // Assert
        var result = _applicationContext.PropertyEntities.FirstOrDefault();
        Assert.Null(result);
    }
}