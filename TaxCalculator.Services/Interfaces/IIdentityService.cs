using TaxCalculator.Domain.DTOs;
using TaxCalculator.Domain.Entities;

namespace TaxCalculator.Services.Interfaces;

public interface IIdentityService {
    Task<User> Login(LoginRequest request);
}
