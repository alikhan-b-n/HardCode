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
        var categoryEntity = await _categoryRepository.GetById(productDto.ProductCategoryDto.CategoryId);
        if(categoryEntity==null)
            throw new ArgumentException("Category not found");
        
        var productEntity = new ProductEntity
        {
            CategoryEntity = categoryEntity,
            CategoryId = productDto.ProductCategoryDto.CategoryId,
            Description = productDto.Description,
            ImageUrl = productDto.ImageUrl,
            Name = productDto.Name,
            Price = productDto.Price
        };


        List<PropertyEntity> propertyEntities = _propertyRepository
            .Query()
            .Where(x => x.CategoryId == productDto.ProductCategoryDto.CategoryId)
            .ToList();

        if (!ValidateProperties(productDto.ProductCategoryDto.Properties, propertyEntities))
            throw new ArgumentException("some of property id is not valid");     
                
        if (propertyEntities.Count != productDto.ProductCategoryDto.Properties.Count)
            throw new ArgumentException("number of properties is not equal to number of value");

        List<ValueEntity> valueEntities = new List<ValueEntity>();

        foreach (var (propertyEntity, property) in propertyEntities.Zip(productDto.ProductCategoryDto.Properties,
                     (propertyEntity, property) => (propertyEntity, property)))
        {
            var valueEntity = new ValueEntity
            {
                ProductEntity = productEntity,
                ProductId = productEntity.Id,
                PropertyEntity = propertyEntity,
                PropertyId = propertyEntity.Id,
                Value = property.Value
            };

            valueEntities.Add(valueEntity);
        }

        await _productRepository.Create(productEntity);
        await _valuesRepository.CreateMany(valueEntities);
    }

    public async Task<ProductDto?> GetProductById(Guid id)
    {
        var productEntity = await _productRepository
            .Query()
            .AsNoTracking()
            .Include(x => x.ValueEntities)
            .ThenInclude(x => x.PropertyEntity)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (productEntity == null)
            return null;

        var productDto = new ProductDto
        {
            Name = productEntity.Name,
            Id = productEntity.Id,
            ProductCategoryDto = new ProductCategoryDto
            {
                CategoryId = productEntity.CategoryId,
                Properties = productEntity.ValueEntities.Select(x => new ProductPropertyDto
                {
                    Id = x.PropertyEntity.Id,
                    Value = x.Value,
                    Name = x.PropertyEntity.Name
                }).ToList()
            },
            Description = productEntity.Description,
            ImageUrl = productEntity.ImageUrl,
            Price = productEntity.Price
        };

        return productDto;
    }

    public async Task<List<ProductDto>> GetProductsByQuery(string? query)
    {
        List<ProductEntity> productEntity;
        if (query == null)
        {
            productEntity = await _productRepository
                .Query()
                .AsNoTracking()
                .Include(x => x.ValueEntities)
                .ThenInclude(x=>x.PropertyEntity)
                .Include(x => x.CategoryEntity)
                .ToListAsync();
        }
        else
        {
            productEntity = await _productRepository
                .Query()
                .AsNoTracking()
                .Include(x => x.ValueEntities)
                .ThenInclude(x=>x.PropertyEntity)
                .Include(x => x.CategoryEntity)
                .Where(x => x.Name.ToLower().Contains(query.ToLower()) ||
                            x.ValueEntities.Any(x => x.Value.ToLower().Contains(query.ToLower())))
                .ToListAsync();
        }


        var dsa = productEntity.Select(x => new ProductDto
        {
            Name = x.Name,
            Id = x.Id,
            ProductCategoryDto = new ProductCategoryDto
            {
                CategoryId = x.CategoryId,
                Properties = x.ValueEntities.Select(x => new ProductPropertyDto
                {
                    Id = x.PropertyEntity.Id,
                    Value = x.Value,
                    Name = x.PropertyEntity.Name
                }).ToList()
            },
            Description = x.Description,
            ImageUrl = x.ImageUrl,
            Price = x.Price
        }).ToList();
        return dsa;
    }
    
    private bool ValidateProperties(List<ProductPropertyDto> propertyDtos, List<PropertyEntity> propertyEntities)
    {
        // Check that every Guid in the list of Guids is present in the list of objects
        if (!propertyDtos.All(propertyDto => propertyEntities.Any(x => x.Id == propertyDto.Id)))
        {
            return false;
        }

        return true;
    }
}