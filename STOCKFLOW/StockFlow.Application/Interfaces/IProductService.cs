using StockFlow.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockFlow.Application.Interfaces
{
    public interface IProductService
    {
        Task<ProductResponseDto?> GetByIdAsync(int id);

        Task<List<ProductResponseDto>> GetAllAsync();

        Task<ProductResponseDto> CreateAsync(CreateProductDto dto);

        Task<ProductResponseDto?> UpdateAsync(int id, UpdateProductDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
