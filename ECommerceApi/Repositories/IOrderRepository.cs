using ECommerceApi.Models.Entities;

namespace ECommerceApi.Repositories;

public interface IOrderRepository
{
    Order Create(int userId, string shippingAddress, decimal totalPrice, List<OrderItem> items);
    List<Order> GetByUserId(int userId);
}