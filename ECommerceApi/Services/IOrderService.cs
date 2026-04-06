using ECommerceApi.Models.Entities;

namespace ECommerceApi.Services;

public interface IOrderService
{
    Order Checkout(int userId, string shippingAddress);
    List<Order> GetUserOrders(int userId);
}