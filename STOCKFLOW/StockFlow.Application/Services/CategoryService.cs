using StockFlow.Application.DTOs;
using StockFlow.Application.Interfaces;
using StockFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockFlow.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(
            ICategoryRepository categoryRepository,
            IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CategoryResponseDto>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            return categories
                .Select(MapToResponse)
                .ToList();
        }

        public async Task<CategoryResponseDto?> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            return category is null
                ? null
                : MapToResponse(category);
        }

        public async Task<CategoryResponseDto> CreateAsync(
            CreateCategoryDto dto)
        {
            var existingCategory =
                await _categoryRepository.GetByNameAsync(dto.Name);

            if (existingCategory is not null)
            {
                throw new InvalidOperationException(
                    "A category with this name already exists.");
            }

            var category = new Category
            {
                Name = dto.Name,
                Description = dto.Description,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _categoryRepository.AddAsync(category);

            await _unitOfWork.SaveChangesAsync();

            return MapToResponse(category);
        }

        public async Task<CategoryResponseDto?> UpdateAsync(
            int id,
            UpdateCategoryDto dto)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category is null)
            {
                return null;
            }

            var existingCategory =
                await _categoryRepository.GetByNameAsync(dto.Name);

            if (existingCategory is not null &&
                existingCategory.Id != id)
            {
                throw new InvalidOperationException(
                    "A category with this name already exists.");
            }

            category.Name = dto.Name;
            category.Description = dto.Description;
            category.UpdatedAt = DateTime.UtcNow;

            _categoryRepository.Update(category);

            await _unitOfWork.SaveChangesAsync();

            return MapToResponse(category);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category is null)
            {
                return false;
            }

            if (category.Products.Count > 0)
            {
                throw new InvalidOperationException(
                    "Cannot delete a category that has products.");
            }

            _categoryRepository.Delete(category);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        private static CategoryResponseDto MapToResponse(
            Category category)
        {
            return new CategoryResponseDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                CreatedAt = category.CreatedAt,
                UpdatedAt = category.UpdatedAt,
                ProductCount = category.Products.Count
            };
        }
    }
}
