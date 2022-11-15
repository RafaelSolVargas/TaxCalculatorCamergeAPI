using System.Text.Json.Serialization;
using TaxCalculator.Domain.Entities;

namespace TaxCalculator.DTOs.Responses;
public class LoginResponse {

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? accessToken { get; set; }

    public int id { get; set; }

    public string email { get; set; }

    public Role role { get; set; }

    public LoginResponse(string? accessToken, User user) {
        this.accessToken = accessToken;
        this.id = (int)user.id!;
        this.email = user.email;
        this.role = user.role;
    }
}

