using ECommerceApi.Models.Entities;

namespace ECommerceApi.Repositories;

public interface IProductRepository
{
    List<Product> GetAll();
    Product? GetById(int id);
}