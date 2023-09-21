using HardCode.Bll.Dtos;
using HardCode.Bll.Services.Interfaces;
using HardCode.Dal.Entites;
using HardCode.Dal.Repositories.Interfaces;

namespace HardCode.Bll.Services;

public class CategoryManager : ICategoryManager
{
    private readonly ICrudRepository<CategoryEntity> _categoryRepository;
    private readonly ICrudRepository<PropertyEntity> _propertyRepository;

    public CategoryManager(ICrudRepository<CategoryEntity> categoryRepository,
        ICrudRepository<PropertyEntity> propertyRepository)
    {
        _categoryRepository = categoryRepository;
        _propertyRepository = propertyRepository;
    }

    public async Task CreateCategory(CategoryDto categoryDto)
    {
        var categoryEntity = new CategoryEntity
        {
            Name = categoryDto.Name
        };

        await _categoryRepository.Create(categoryEntity);

        await _propertyRepository.CreateMany(categoryDto.Properties
            .Select(x => new PropertyEntity
            {
                Name = x.Name,
                CategoryEntity = categoryEntity,
                CategoryId = categoryEntity.Id
            })
            .ToList());
    }

    public async Task DeleteCategory(Guid id)
    {
        await _categoryRepository.Delete(id);
        // will not have to delete properties because of cascading delete
    }

    public async Task<List<CategoryDto>> GetAllCategories()
    {
        var categoryEntities = await _categoryRepository.GetAll();
        var categoryDtos = new List<CategoryDto>();
        
        foreach (var categoryEntity in categoryEntities)
        {
            var properties = _propertyRepository
                .Query()
                .Where(x => x.CategoryId == categoryEntity.Id)
                .Select(prop => new Property
                {
                    Name = prop.Name,
                    Type = prop.Type
                }).ToList();

            categoryDtos.Add(new CategoryDto
            {
                Name = categoryEntity.Name,
                Properties = properties
            });
        }

        return categoryDtos;
    }
}