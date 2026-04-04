using ECommerceApi.Models.Entities;

namespace ECommerceApi.Repositories;

public interface IUserRepository
{
    User? GetByEmail(string email);
    User Create(string name, string email, string passwordHash);
}