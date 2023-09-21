using Microsoft.AspNetCore.Mvc;
using TechTaskHardCode.ViewModels.Parameters;

namespace TechTaskHardCode.Controllers;

public class CategoryController : ControllerBase
{
    [HttpPost("api/categories")]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryModel model)
    {
        return Ok();
    }

    [HttpGet("api/categories/{id:guid}")]
    public async Task<IActionResult> GetCategory()
    {
        return Ok();
    }

    [HttpDelete("api/categories/{id:guid}")]
    public async Task<IActionResult> DeleteCategory()
    {
        return Ok();
    }
}