using StockFlow.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockFlow.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto dto);

        Task<AuthResponseDto> LoginAsync(LoginDto dto);
    }
}
