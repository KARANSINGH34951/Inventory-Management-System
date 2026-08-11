using StockFlow.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockFlow.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryResponseDto>> GetAllAsync();

        Task<CategoryResponseDto?> GetByIdAsync(int id);

        Task<CategoryResponseDto> CreateAsync(CreateCategoryDto dto);

        Task<CategoryResponseDto?> UpdateAsync(
            int id,
            UpdateCategoryDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
