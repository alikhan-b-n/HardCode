using Microsoft.AspNetCore.Mvc;
using TechTaskHardCode.ViewModels.Parameters;

namespace TechTaskHardCode.Controllers;

public class ProductController : ControllerBase
{
    [HttpPost("api/products")]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductModel model)
    {
        return Ok();
    }

    [HttpGet("api/products/")]
    public async Task<IActionResult> GetProducts([FromQuery] string query)
    {
        return Ok();
    }

    [HttpGet("api/products/{id:guid}")]
    public async Task<IActionResult> GetByIdProduct([FromRoute] Guid id)
    {
        return Ok();
    }
}