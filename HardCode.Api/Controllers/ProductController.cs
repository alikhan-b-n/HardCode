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
        try
        {
            var productPropertyDto = paramModel
                .ProductCategoryParamModel
                .Properties
                .Select(x =>
                    new ProductPropertyDto
                    {
                        Id = x.Id,
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
                    CategoryId = paramModel.ProductCategoryParamModel.CategoryId,
                    Properties = productPropertyDto
                }
            });

            return NoContent();
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
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
            ProductCategoryParamModel = new ProductCategoryResponseModel
            {
                CategoryId = x.ProductCategoryDto.CategoryId,
                Properties = x.ProductCategoryDto
                    .Properties
                    .Select(x => new
                        ProductPropertyResponseModel { Name = x.Name, Value = x.Value, Id = x.Id})
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
            ProductCategoryParamModel = new ProductCategoryResponseModel
            {
                CategoryId = productDto.ProductCategoryDto.CategoryId,
                Properties = productDto.ProductCategoryDto
                    .Properties
                    .Select(x => new
                        ProductPropertyResponseModel { Name = x.Name, Value = x.Value, Id = x.Id})
                    .ToList()
            }
        };

        return Ok(productViewModel);
    }
}