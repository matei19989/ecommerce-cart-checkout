using System.ComponentModel.DataAnnotations;

namespace ECommerceApi.Models.DTOs;

public class LoginRequest
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required.")]
    public string Password { get; set; } = string.Empty;
}