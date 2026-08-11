using StockFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockFlow.Application.Interfaces
{
    public interface IAuthRepository
    {
        Task<User?> GetByEmailAsync(string email);

        Task AddAsync(User user);
    }
}
