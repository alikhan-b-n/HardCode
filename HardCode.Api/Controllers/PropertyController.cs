using HardCode.Bll.Dtos;
using HardCode.Bll.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TechTaskHardCode.ViewModels;

namespace TechTaskHardCode.Controllers;

public class PropertyController : ControllerBase
{
    private readonly IPropertyManager _manager;

    public PropertyController(IPropertyManager manager)
    {
        _manager = manager;
    }

    [HttpPost("api/property")]
    public async Task<IActionResult> CreateProperty([FromBody] PropertyParamModel propertyParamModel)
    {
        try
        {
            await _manager.Create(new PropertyDto
            {
                CategoryId = propertyParamModel.CategoryId,
                Name = propertyParamModel.Name
            });

            return NoContent();
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("api/property/{id:guid}")]
    public async Task<IActionResult> DeleteProperty([FromRoute] Guid id)
    {
        await _manager.Delete(id);
        return NoContent();
    }
}