using HardCode.Bll.Dtos;
using HardCode.Bll.Services.Interfaces;
using HardCode.Dal.Entites;
using HardCode.Dal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HardCode.Bll.Services;

public class ProductManager : IProductManager
{
    private readonly ICrudRepository<ProductEntity> _productRepository;
    private readonly ICrudRepository<ValueEntity> _valuesRepository;
    private readonly ICrudRepository<PropertyEntity> _propertyRepository;
    private readonly ICrudRepository<CategoryEntity> _categoryRepository;

    public ProductManager(ICrudRepository<ProductEntity> productRepository,
        ICrudRepository<ValueEntity> valuesRepository,
        ICrudRepository<PropertyEntity> propertyRepository,
        ICrudRepository<CategoryEntity> categoryRepository)
    {
        _productRepository = productRepository;
        _valuesRepository = valuesRepository;
        _propertyRepository = propertyRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task CreateProduct(ProductDto productDto)
    {
        var categoryEntity = await _categoryRepository.GetById(productDto.Category.CategoryId);
        var productEntity = new ProductEntity
        {
            CategoryEntity = categoryEntity,
            CategoryId = productDto.Category.CategoryId,
            Description = productDto.Description,
            ImageUrl = productDto.ImageUrl,
            Name = productDto.Name,
            Price = productDto.Price
        };

        List<PropertyEntity> propertyEntities = new List<PropertyEntity>();
        List<ValueEntity> valueEntities = new List<ValueEntity>();

        foreach (var property in productDto.Category.Properties)
        {
            var propertyEntity = new PropertyEntity
            {
                CategoryEntity = categoryEntity,
                CategoryId = productDto.Category.CategoryId,
                Name = property.Name,
                Type = property.Type
            };

            var valueEntity = new ValueEntity
            {
                Value = property.Value,
                ProductEntity = productEntity,
                ProductId = productEntity.Id,
                PropertyEntity = propertyEntity,
                PropertyId = propertyEntity.Id
            };
            propertyEntities.Add(propertyEntity);
            valueEntities.Add(valueEntity);
        }

        await _productRepository.Create(productEntity);
        await _valuesRepository.CreateMany(valueEntities);
        await _propertyRepository.CreateMany(propertyEntities);
    }

    public async Task<ProductDto?> GetProductById(Guid id)
    {
        var productEntity = await _productRepository
            .Query()
            .Include(x => x.ValueEntities)
            .ThenInclude(x=>x.PropertyEntity)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (productEntity == null)
            return null;
        
        var productDto = new ProductDto
        {
            Name = productEntity.Name,
            Category = new Category
            {
                CategoryId = productEntity.CategoryId,
                Properties = productEntity.ValueEntities.Select(x=>new PropertyProduct
                {
                    Name = x.PropertyEntity.Name,
                    Type = x.PropertyEntity.Type,
                    Value = x.Value
                }).ToList()
            },
            Description = productEntity.Description,
            ImageUrl = productEntity.ImageUrl,
            Price = productEntity.Price
        };

        return productDto;
    }

    public async Task<List<ProductDto>> GetProductsByQuery(string query)
    {
        var productEntity = await _productRepository
            .Query()
            .Include(x => x.ValueEntities)
            .Include(x => x.CategoryEntity)
            .Where(x=>
                x.CategoryEntity.Name.Contains(query) || 
                x.ValueEntities.Any(value=>value.Value.Contains(query)))
            .ToListAsync();
        
        return productEntity.Select(productEntity => new ProductDto
        {
            Name = productEntity.Name,
            Category = new Category
            {
                CategoryId = productEntity.CategoryId,
                Properties = productEntity.ValueEntities.Select(x=>new PropertyProduct
                {
                    Name = x.PropertyEntity.Name,
                    Type = x.PropertyEntity.Type,
                    Value = x.Value
                }).ToList()
            },
            Description = productEntity.Description,
            ImageUrl = productEntity.ImageUrl,
            Price = productEntity.Price
        }).ToList();
    }
}