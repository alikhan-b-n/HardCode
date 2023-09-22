using HardCode.Bll.Dtos;

namespace HardCode.Bll.Services.Interfaces;

public interface IProductManager
{
    public Task<List<ProductDto>> GetProductsByQuery(string? query);
    public Task CreateProduct(ProductDto productDto);
    public Task<ProductDto?> GetProductById(Guid id);
}