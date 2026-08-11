using StockFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockFlow.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(int id);

        Task<List<Product>> GetAllAsync();

        Task<Product?> GetBySkuAsync(string sku);

        Task AddAsync(Product product);

        void Update(Product product);

        void Delete(Product product);

        Task<bool> ExistsAsync(int id);
    }
}
