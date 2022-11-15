namespace TaxCalculator.Domain.Entities;

public class Role {
    public bool canCalculateTax { get; set; }

    public Role(bool canCalculateTax) {
        this.canCalculateTax = canCalculateTax;
    }
}
