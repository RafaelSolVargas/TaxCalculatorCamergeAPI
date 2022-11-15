using TaxCalculator.Domain.Entities;

namespace TaxCalculator.Repositories.Interfaces;

public interface ITaxCalculatorRepository {
    Task<Tax> GetInterestRateAsync();
}
