using HardCode.Bll.Dtos;

namespace HardCode.Bll.Services.Interfaces;

public interface ICategoryManager
{
    public Task DeleteCategory(Guid id);
    public Task<List<CategoryDto>> GetAllCategories();
    public Task CreateCategory(CategoryDto categoryDto);
}