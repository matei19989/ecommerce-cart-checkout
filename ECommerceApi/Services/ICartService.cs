using ECommerceApi.Models.DTOs;

namespace ECommerceApi.Services;

public interface ICartService
{
    List<CartItemResponse> GetCart(int userId);
    void AddToCart(int userId, CartItemDto item);
    void UpdateQuantity(int userId, int productId, int quantity);
    void RemoveFromCart(int userId, int productId);
}