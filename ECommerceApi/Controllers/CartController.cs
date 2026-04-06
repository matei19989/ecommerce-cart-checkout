using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ECommerceApi.Models.DTOs;
using ECommerceApi.Services;

namespace ECommerceApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CartController : BaseAuthController
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet]
    public IActionResult GetCart()
    {
        return Ok(_cartService.GetCart(GetUserId()));
    }

    [HttpPost]
    public IActionResult AddToCart( CartItemDto item)
    {
            _cartService.AddToCart(GetUserId(), item);
            return Ok(new { message = "Product added to cart." });
        }

    [HttpPut("{productId}")]
    public IActionResult UpdateQuantity(int productId, [FromBody] CartItemDto item)
    {
        _cartService.UpdateQuantity(GetUserId(), productId, item.Quantity);
        return Ok(new { message = "Cart updated." });
    }

    [HttpDelete("{productId}")]
    public IActionResult RemoveFromCart(int productId)
    {
        _cartService.RemoveFromCart(GetUserId(), productId);
        return Ok(new { message = "Product removed from cart." });
    }
}