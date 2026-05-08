using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ECommerceApi.Models.DTOs;
using ECommerceApi.Services;

namespace ECommerceApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly ICurrentUser _currentUser;

    public OrdersController(IOrderService orderService, ICurrentUser currentUser)
    {
        _orderService = orderService;
        _currentUser = currentUser;
    }

    [HttpPost("checkout")]
    public IActionResult Checkout(CheckoutRequest request) =>
        Ok(_orderService.Checkout(_currentUser.Id, request.ShippingAddress));

    [HttpGet]
    public IActionResult GetOrders() =>
        Ok(_orderService.GetUserOrders(_currentUser.Id));
}
