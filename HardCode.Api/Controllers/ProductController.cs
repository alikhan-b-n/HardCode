using HardCode.Bll.Dtos;
using HardCode.Bll.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TechTaskHardCode.ViewModels;

namespace TechTaskHardCode.Controllers;

public class ProductController : ControllerBase
{
    private readonly IProductManager _productManager;

    public ProductController(IProductManager productManager)
    {
        _productManager = productManager;
    }

    [HttpPost("api/products")]
    public async Task<IActionResult> CreateProduct([FromBody] ProductParamModel paramModel)
    {
        var productPropertyDto = paramModel
            .ProductCategoryViewModel
            .Properties
            .Select(x =>
                new ProductPropertyDto
                {
                    Name = x.Name,
                    Type = x.Type,
                    Value = x.Value
                })
            .ToList();

        await _productManager.CreateProduct(new ProductDto
        {
            Description = paramModel.Description,
            ImageUrl = paramModel.ImageUrl,
            Name = paramModel.Name,
            Price = paramModel.Price,
            ProductCategoryDto = new ProductCategoryDto
            {
                CategoryId = paramModel.ProductCategoryViewModel.CategoryId,
                Properties = productPropertyDto
            }
        });

        return NoContent();
    }

    [HttpGet("api/products/")]
    public async Task<IActionResult> GetProducts([FromQuery] string? query)
    {
        var productDtos = await _productManager.GetProductsByQuery(query);
        var productViewModels = productDtos.Select(x => new ProductResponseModel
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            ImageUrl = x.ImageUrl,
            Price = x.Price,
            ProductCategoryViewModel = new ProductCategoryViewModel
            {
                CategoryId = x.ProductCategoryDto.CategoryId,
                Properties = x.ProductCategoryDto
                    .Properties
                    .Select(x => new
                        ProductPropertyViewModel { Name = x.Name, Type = x.Type, Value = x.Value })
                    .ToList()
            }
        }).ToList();

        return Ok(productViewModels);
    }

    [HttpGet("api/products/{id:guid}")]
    public async Task<IActionResult> GetByIdProduct([FromRoute] Guid id)
    {
        var productDto = await _productManager.GetProductById(id);
        if (productDto == null)
            return Ok(new
            {
                Message = "Product not found"
            });
        
        var productViewModel = new ProductResponseModel
        {
            Id = productDto.Id,
            Description = productDto.Description,
            ImageUrl = productDto.ImageUrl,
            Name = productDto.Name,
            Price = productDto.Price,
            ProductCategoryViewModel = new ProductCategoryViewModel
            {
                CategoryId = productDto.ProductCategoryDto.CategoryId,
                Properties = productDto.ProductCategoryDto
                    .Properties
                    .Select(x => new
                        ProductPropertyViewModel { Name = x.Name, Type = x.Type, Value = x.Value })
                    .ToList()
            }
        };

        return Ok(productViewModel);
    }
}