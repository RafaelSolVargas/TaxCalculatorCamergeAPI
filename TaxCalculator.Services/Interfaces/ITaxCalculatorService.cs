namespace TaxCalculator.Services.Interfaces;

public interface ITaxCalculatorService {
    Task<double> CalculateTaxAsync(double initialValue, int months);
}
