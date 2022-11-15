using System.ComponentModel.DataAnnotations;

namespace TaxCalculator.Domain.DTOs;

public class LoginRequest {
    [Required(ErrorMessage = "The field {0} is required")]
    public string? email { get; set; }

    [Required(ErrorMessage = "The field {0} is required")]
    [MinLength(8)] // The password must have at least 8 characters
    public string? password { get; set; }
}
