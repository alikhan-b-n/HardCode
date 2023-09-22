using HardCode.Bll.Dtos;
using HardCode.Bll.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TechTaskHardCode.ViewModels;

namespace TechTaskHardCode.Controllers;

public class CategoryController : ControllerBase
{
    private readonly ICategoryManager _manager;

    public CategoryController(ICategoryManager manager)
    {
        _manager = manager;
    }

    [HttpPost("api/categories")]
    public async Task<IActionResult> CreateCategory([FromBody] CategoryParamModel paramModel)
    {
        var properties = paramModel
            .Properties
            .Select(x =>
                new CategoryPropertyDto { Name = x.Name, Type = x.Type })
            .ToList();

        await _manager.CreateCategory(new CategoryDto
        {
            Name = paramModel.Name,
            Properties = properties
        });

        return NoContent();
    }

    [HttpGet("api/categories")]
    public async Task<IActionResult> GetCategories()
    {
        var categoryDtos = await _manager.GetAllCategories();

        return Ok(categoryDtos.Select(x => new CategoryResponseModel
        {
            Name = x.Name,
            Id = x.Id,
            Properties = x.Properties.Select(x => new CategoryPropertyViewModel
            {
                Name = x.Name,
                Type = x.Type
            }).ToList()
        }));
    }

    [HttpDelete("api/categories/{id:guid}")]
    public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
    {
        await _manager.DeleteCategory(id);
        return NoContent();
    }
}