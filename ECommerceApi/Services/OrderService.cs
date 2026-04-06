using ECommerceApi.Models.Entities;
using ECommerceApi.Repositories;

namespace ECommerceApi.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;

    public OrderService(IOrderRepository orderRepository, ICartRepository cartRepository, IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _cartRepository = cartRepository;
        _productRepository = productRepository;
    }

    public Order Checkout(int userId, string shippingAddress)
    {
        var cartItems = _cartRepository.GetByUserId(userId);
        if (cartItems.Count == 0)
            throw new Exception("Cart is empty.");

        // calculate total from product database, NOT from frontend
        var orderItems = new List<OrderItem>();
        decimal totalPrice = 0;

        foreach (var cartItem in cartItems)
        {
            var product = _productRepository.GetById(cartItem.ProductId);
            if (product == null) continue;

            var unitPrice = product.Price;
            totalPrice += unitPrice * cartItem.Quantity;

            orderItems.Add(new OrderItem
            {
                ProductId = cartItem.ProductId,
                Quantity = cartItem.Quantity,
                UnitPrice = unitPrice
            });
        }

        var order = _orderRepository.Create(userId, shippingAddress, totalPrice, orderItems);

        _cartRepository.ClearCart(userId);

        return order;
    }

    public List<Order> GetUserOrders(int userId) => _orderRepository.GetByUserId(userId);
}