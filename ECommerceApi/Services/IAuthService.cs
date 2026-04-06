using ECommerceApi.Models.DTOs;

namespace ECommerceApi.Services;

public interface IAuthService
{
    AuthResponse Register(RegisterRequest request);
    AuthResponse Login(LoginRequest request);
}