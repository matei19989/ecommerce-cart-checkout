using System.ComponentModel.DataAnnotations;

namespace ECommerceApi.Models.DTOs;

public class RegisterRequest
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required.")]
    public string Name { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required.")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
    public string Password { get; set; } = string.Empty;
}