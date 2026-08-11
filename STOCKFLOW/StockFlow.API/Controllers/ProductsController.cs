using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockFlow.Application.DTOs;
using StockFlow.Application.Interfaces;

namespace StockFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    // User + Admin
    [HttpGet]
    public async Task<ActionResult<List<ProductResponseDto>>> GetAll()
    {
        var products = await _productService.GetAllAsync();

        return Ok(products);
    }

    // User + Admin
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductResponseDto>> GetById(int id)
    {
        var product = await _productService.GetByIdAsync(id);

        if (product is null)
        {
            return NotFound(new
            {
                message = "Product not found."
            });
        }

        return Ok(product);
    }

    // Admin only
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<ProductResponseDto>> Create(
        CreateProductDto dto)
    {
        try
        {
            var product = await _productService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = product.Id },
                product);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new
            {
                message = ex.Message
            });
        }
    }

    // Admin only
    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<ActionResult<ProductResponseDto>> Update(
        int id,
        UpdateProductDto dto)
    {
        try
        {
            var product = await _productService.UpdateAsync(id, dto);

            if (product is null)
            {
                return NotFound(new
                {
                    message = "Product not found."
                });
            }

            return Ok(product);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new
            {
                message = ex.Message
            });
        }
    }

    // Admin only
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _productService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound(new
            {
                message = "Product not found."
            });
        }

        return NoContent();
    }
}