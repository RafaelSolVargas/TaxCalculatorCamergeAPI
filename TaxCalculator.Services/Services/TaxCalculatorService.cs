using TaxCalculator.Repositories.Interfaces;
using TaxCalculator.Services.Interfaces;

namespace TaxCalculator.Services;

public class TaxCalculatorService : ITaxCalculatorService {
    private ITaxCalculatorRepository calculatorRepo;

    public TaxCalculatorService(ITaxCalculatorRepository calculatorRepo) {
        this.calculatorRepo = calculatorRepo;
    }

    public async Task<double> CalculateTaxAsync(double initialValue, int months) {
        var tax = await this.calculatorRepo.GetInterestRateAsync();

        return (initialValue * Math.Pow((1 + tax.interestRate), months));
    }
}
