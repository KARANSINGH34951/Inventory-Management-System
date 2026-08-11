using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockFlow.Application.DTOs;
using StockFlow.Application.Interfaces;

namespace StockFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    // User + Admin
    [HttpGet]
    public async Task<ActionResult<List<CategoryResponseDto>>> GetAll()
    {
        var categories = await _categoryService.GetAllAsync();

        return Ok(categories);
    }

    // User + Admin
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CategoryResponseDto>> GetById(int id)
    {
        var category = await _categoryService.GetByIdAsync(id);

        if (category is null)
        {
            return NotFound(new
            {
                message = "Category not found."
            });
        }

        return Ok(category);
    }

    // Admin only
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<CategoryResponseDto>> Create(
        CreateCategoryDto dto)
    {
        try
        {
            var category = await _categoryService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = category.Id },
                category);
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
    public async Task<ActionResult<CategoryResponseDto>> Update(
        int id,
        UpdateCategoryDto dto)
    {
        try
        {
            var category = await _categoryService.UpdateAsync(id, dto);

            if (category is null)
            {
                return NotFound(new
                {
                    message = "Category not found."
                });
            }

            return Ok(category);
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
        try
        {
            var deleted = await _categoryService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound(new
                {
                    message = "Category not found."
                });
            }

            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new
            {
                message = ex.Message
            });
        }
    }
}