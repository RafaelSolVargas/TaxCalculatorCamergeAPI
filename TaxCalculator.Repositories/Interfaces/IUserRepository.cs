using TaxCalculator.Domain.Entities;

namespace TaxCalculator.Repositories.Interfaces;

public interface IUserRepository {
    User Get(int id);
}
