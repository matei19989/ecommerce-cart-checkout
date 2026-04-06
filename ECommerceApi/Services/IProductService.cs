using ECommerceApi.Models.Entities;

namespace ECommerceApi.Services;

public interface IProductService
{
    List<Product> GetAll();
    Product? GetById(int id);
}