using Moq;
using ECommerceApi.Services;
using ECommerceApi.Repositories;
using ECommerceApi.Models.DTOs;
using ECommerceApi.Models.Entities;

namespace ECommerceApi.Tests;

public class OrderServiceTests
{
    private readonly Mock<IOrderRepository> _orderRepo;
    private readonly Mock<ICartRepository> _cartRepo;
    private readonly Mock<IProductRepository> _productRepo;
    private readonly OrderService _orderService;

    public OrderServiceTests()
    {
        _orderRepo = new Mock<IOrderRepository>();
        _cartRepo = new Mock<ICartRepository>();
        _productRepo = new Mock<IProductRepository>();
        _orderService = new OrderService(_orderRepo.Object, _cartRepo.Object, _productRepo.Object);
    }

    [Fact]
    public void Checkout_CalculatesTotalFromDatabase_NotFromFrontend()
    {
        var cartItems = new List<CartItemResponse>
        {
            new() { ProductId = 1, Quantity = 2, Price = 999m },
            new() { ProductId = 2, Quantity = 1, Price = 999m }
        };

        _cartRepo.Setup(r => r.GetByUserId(1)).Returns(cartItems);
        _productRepo.Setup(r => r.GetById(1)).Returns(new Product { Id = 1, Price = 10m });
        _productRepo.Setup(r => r.GetById(2)).Returns(new Product { Id = 2, Price = 25m });

        _orderRepo.Setup(r => r.Create(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<List<OrderItem>>()))
            .Returns((int userId, string addr, decimal total, List<OrderItem> items) =>
                new Order { Id = 1, UserId = userId, TotalPrice = total, ShippingAddress = addr, Items = items });

        var order = _orderService.Checkout(1, "123 Test St");

        // total should be (10 * 2) + (25 * 1) = 45, NOT the 999 from cart
        Assert.Equal(45m, order.TotalPrice);
    }

    [Fact]
    public void Checkout_ThrowsWhenCartIsEmpty()
    {
        _cartRepo.Setup(r => r.GetByUserId(1)).Returns(new List<CartItemResponse>());

        Assert.Throws<InvalidOperationException>(() => _orderService.Checkout(1, "123 Test St"));
    }

    [Fact]
    public void Checkout_ThrowsWhenProductNotFound()
    {
        var cartItems = new List<CartItemResponse>
        {
            new() { ProductId = 99, Quantity = 1 }
        };

        _cartRepo.Setup(r => r.GetByUserId(1)).Returns(cartItems);
        _productRepo.Setup(r => r.GetById(99)).Returns((Product?)null);

        Assert.Throws<KeyNotFoundException>(() => _orderService.Checkout(1, "123 Test St"));
    }

    [Fact]
    public void Checkout_ClearsCartAfterSuccess()
    {
        var cartItems = new List<CartItemResponse>
        {
            new() { ProductId = 1, Quantity = 1 }
        };

        _cartRepo.Setup(r => r.GetByUserId(1)).Returns(cartItems);
        _productRepo.Setup(r => r.GetById(1)).Returns(new Product { Id = 1, Price = 10m });
        _orderRepo.Setup(r => r.Create(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<List<OrderItem>>()))
            .Returns(new Order { Id = 1, TotalPrice = 10m });

        _orderService.Checkout(1, "123 Test St");

        _cartRepo.Verify(r => r.ClearCart(1), Times.Once);
    }
}