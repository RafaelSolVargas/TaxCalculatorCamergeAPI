using TaxCalculator.Repositories.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using TaxCalculator.Domain.Entities;

// The IMemoryCache interface provided by .NET is for a local memory, so it's not big and if it's used too much it could
// affect the Memory of the API, and it's not distributed, so if there are multiple instances of API running they will not
// share the same cache.
namespace TaxCalculator.Repositories;
public class TaxCalculatorCache : ITaxCalculatorRepository {
    private readonly IMemoryCache memoryCache;
    private readonly ITaxCalculatorRepository innerRepository;
    private readonly TimeSpan expirationTime;

    public TaxCalculatorCache(IMemoryCache memoryCache, ITaxCalculatorRepository innerRepository) {
        this.memoryCache = memoryCache;
        this.innerRepository = innerRepository;
        // We set a minimum expirationTime to not keep storing out of date information in case the interestRate changes
        // Uma solução para melhorarmos a eficiência ao diminuir a quantidade de acessos à TaxAPI é aumentarmos a duração da
        // informação na cache e implementarmos um serviço de mensageiria, como por exemplo, RabbitMQ para envio de
        // notificações, e a TaxAPI iria enviar notificações por meio do RabbitMQ informando quando houve uma alteração da Tax
        this.expirationTime = TimeSpan.FromMinutes(1);
    }

    public async Task<Tax> GetInterestRateAsync() {
        var interestRate = this.memoryCache.Get<Tax>("interestRate");

        if (interestRate == null) {
            interestRate = await this.innerRepository.GetInterestRateAsync();

            this.memoryCache.Set("interestRate", interestRate, this.expirationTime);
        }

        return interestRate;
    }
}
