namespace TaxCalculator.Domain.Configurations;

public class JwtOptions {
    public string? Issuer { get; set; }
    public string? Audience { get; set; }
    public string? JwtSecretKey { get; set; }
    public int AccessTokenExpirationHours { get; set; }
}
