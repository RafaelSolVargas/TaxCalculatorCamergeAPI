using TaxCalculator.Domain.Entities;

namespace TaxCalculator.Services.Interfaces;

public interface IUserService {
    User Get(int id);
}
