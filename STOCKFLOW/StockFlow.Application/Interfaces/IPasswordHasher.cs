using System;
using System.Collections.Generic;
using System.Text;

namespace StockFlow.Application.Interfaces
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);

        bool VerifyPassword(string password, string passwordHash);
    }
}
