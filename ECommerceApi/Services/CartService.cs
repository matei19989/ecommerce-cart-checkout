using ECommerceApi.Models.DTOs;
using ECommerceApi.Repositories;

namespace ECommerceApi.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;

    public CartService(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public List<CartItemResponse> GetCart(int userId) => _cartRepository.GetByUserId(userId);

    public void AddToCart(int userId, CartItemDto item)
    {
        _cartRepository.AddItem(userId, item.ProductId, item.Quantity);
    }

    public void UpdateQuantity(int userId, int productId, int quantity)
    {
        if (quantity <= 0)
        {
            _cartRepository.RemoveItem(userId, productId);
            return;
        }
        _cartRepository.UpdateQuantity(userId, productId, quantity);
    }

    public void RemoveFromCart(int userId, int productId)
    {
        _cartRepository.RemoveItem(userId, productId);
    }
}