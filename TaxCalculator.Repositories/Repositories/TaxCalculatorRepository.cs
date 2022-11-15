using TaxCalculator.Repositories.Interfaces;
using TaxCalculator.Domain.Entities;
using TaxCalculator.Domain.Configurations;

namespace TaxCalculator.Repositories;

public class TaxCalculatorRepository : ITaxCalculatorRepository {
    private HttpClient httpClient;
    private string taxApiURL;
    private string interestRateEndpoint = "/interestRate";

    public TaxCalculatorRepository() {
        this.taxApiURL = Settings.GetInstance().taxApiURL!;
        this.httpClient = new HttpClient();
    }

    public async Task<Tax> GetInterestRateAsync() {
        var response = await this.httpClient.GetAsync($"{this.taxApiURL}{this.interestRateEndpoint}");

        return new Tax(5);
    }
}
