using TaxCalculator.Domain.Entities;

namespace TaxCalculator.Services.Interfaces;

public interface ITokenService {
    User ValidateAccessToken(string token);

    string GenerateAccessToken(User user);
}
