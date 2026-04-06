using ECommerceApi.Models.Entities;
using ECommerceApi.Repositories;

namespace ECommerceApi.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public List<Product> GetAll() => _productRepository.GetAll();
    public Product? GetById(int id) => _productRepository.GetById(id);
}