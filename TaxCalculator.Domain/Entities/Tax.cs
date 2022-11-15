namespace TaxCalculator.Domain.Entities;

public class Tax {
    public double interestRate { get; set; }

    public Tax(double interestRate) {
        this.interestRate = interestRate;
    }
}
