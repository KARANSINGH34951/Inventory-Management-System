using StockFlow.Application.DTOs.Auth;
using StockFlow.Application.Interfaces;
using StockFlow.Domain.Entities;
using StockFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockFlow.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(
            IAuthRepository authRepository,
            IPasswordHasher passwordHasher,
            IJwtTokenGenerator jwtTokenGenerator,
            IUnitOfWork unitOfWork)
        {
            _authRepository = authRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            var existingUser = await _authRepository.GetByEmailAsync(dto.Email);

            if (existingUser is not null)
            {
                throw new InvalidOperationException(
                    "A user with this email already exists.");
            }

            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = _passwordHasher.HashPassword(dto.Password),
                Role = UserRole.User,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _authRepository.AddAsync(user);

            await _unitOfWork.SaveChangesAsync();

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthResponseDto
            {
                Token = token,
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role.ToString()
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _authRepository.GetByEmailAsync(dto.Email);

            if (user is null)
            {
                throw new UnauthorizedAccessException(
                    "Invalid email or password.");
            }

            var passwordValid = _passwordHasher.VerifyPassword(
                dto.Password,
                user.PasswordHash);

            if (!passwordValid)
            {
                throw new UnauthorizedAccessException(
                    "Invalid email or password.");
            }

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthResponseDto
            {
                Token = token,
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role.ToString()
            };
        }
    }
}
