using StockFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockFlow.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
