using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ECommerceApi.Auth;
using ECommerceApi.Models.DTOs;
using ECommerceApi.Services;

namespace ECommerceApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;
    private readonly ICurrentUser _currentUser;

    public CartController(ICartService cartService, ICurrentUser currentUser)
    {
        _cartService = cartService;
        _currentUser = currentUser;
    }

    [HttpGet]
    public IActionResult GetCart() =>
        Ok(_cartService.GetCart(_currentUser.Id));

    [HttpPost]
    public IActionResult AddToCart(CartItemDto item)
    {
        _cartService.AddToCart(_currentUser.Id, item);
        return Ok(new { message = "Product added to cart." });
    }

    [HttpPut("{productId}")]
    public IActionResult UpdateQuantity(int productId, [FromBody] CartItemDto item)
    {
        _cartService.UpdateQuantity(_currentUser.Id, productId, item.Quantity);
        return Ok(new { message = "Cart updated." });
    }

    [HttpDelete("{productId}")]
    public IActionResult RemoveFromCart(int productId)
    {
        _cartService.RemoveFromCart(_currentUser.Id, productId);
        return Ok(new { message = "Product removed from cart." });
    }
}
