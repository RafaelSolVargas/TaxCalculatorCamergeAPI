using TaxCalculator.Domain.DTOs;
using TaxCalculator.Domain.Entities;

namespace TaxCalculator.Services.Interfaces;

public interface IIdentityService {
    User? Login(LoginRequest request);
}
