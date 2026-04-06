using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ECommerceApi.Models.DTOs;
using ECommerceApi.Services;

namespace ECommerceApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrdersController : BaseAuthController
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost("checkout")]
    public IActionResult Checkout(CheckoutRequest request)
    {
        var order = _orderService.Checkout(GetUserId(), request.ShippingAddress);
        return Ok(order);
    }

    [HttpGet]
    public IActionResult GetOrders()
    {
        return Ok(_orderService.GetUserOrders(GetUserId()));
    }
}