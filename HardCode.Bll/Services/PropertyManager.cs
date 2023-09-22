using HardCode.Bll.Dtos;
using HardCode.Bll.Services.Interfaces;
using HardCode.Dal.Entites;
using HardCode.Dal.Repositories.Interfaces;

namespace HardCode.Bll.Services;

public class PropertyManager : IPropertyManager
{
    private readonly ICrudRepository<PropertyEntity> _propertyRepository;
    private readonly ICrudRepository<CategoryEntity> _categoryRepository;

    public PropertyManager(ICrudRepository<PropertyEntity> propertyRepository,
        ICrudRepository<CategoryEntity> categoryRepository)
    {
        _propertyRepository = propertyRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task Delete(Guid id)
    {
        await _propertyRepository.Delete(id);
    }

    public async Task Create(PropertyDto propertyDto)
    {
        var categoryEntity = await _categoryRepository.GetById(propertyDto.CategoryId);

        if (categoryEntity == null)
            throw new ArgumentException("category not found");
        
        await _propertyRepository.Create(new PropertyEntity
        {
            Name = propertyDto.Name,
            CategoryEntity = categoryEntity,
            CategoryId = categoryEntity.Id
        });
    }
}