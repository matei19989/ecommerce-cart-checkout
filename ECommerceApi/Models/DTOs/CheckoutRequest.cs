using System.ComponentModel.DataAnnotations;

namespace ECommerceApi.Models.DTOs;

public class CheckoutRequest
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Shipping address is required.")]
    [MinLength(5, ErrorMessage = "Shipping address must be at least 5 characters.")]
    public string ShippingAddress { get; set; } = string.Empty;
}