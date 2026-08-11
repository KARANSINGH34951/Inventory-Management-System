using StockFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockFlow.Application.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category?> GetByIdAsync(int id);

        Task<List<Category>> GetAllAsync();

        Task<Category?> GetByNameAsync(string name);

        Task AddAsync(Category category);

        void Update(Category category);

        void Delete(Category category);
    }
}
