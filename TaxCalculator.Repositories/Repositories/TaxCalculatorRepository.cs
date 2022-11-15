using TaxCalculator.Repositories.Interfaces;
using TaxCalculator.Domain.Entities;
using TaxCalculator.Domain.Configurations;
using TaxCalculator.Exceptions;
using Newtonsoft.Json;

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
        try {
            var response = await this.httpClient.GetAsync($"{this.taxApiURL}{this.interestRateEndpoint}");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
                throw new DatabaseOfflineError(content);

            var tax = JsonConvert.DeserializeObject<Tax>(await response.Content.ReadAsStringAsync());

            return tax!;
        } catch (Exception error) {
            throw new DatabaseOfflineError(error.Message);
        }
    }
}
