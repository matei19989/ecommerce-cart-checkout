using ECommerceApi.Models.DTOs;

namespace ECommerceApi.Repositories;

public interface ICartRepository
{
    List<CartItemResponse> GetByUserId(int userId);
    void AddItem(int userId, int productId, int quantity);
    void UpdateQuantity(int userId, int productId, int quantity);
    void RemoveItem(int userId, int productId);
    void ClearCart(int userId);
}