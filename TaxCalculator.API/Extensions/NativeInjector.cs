using TaxCalculator.Services.Interfaces;
using TaxCalculator.Repositories.Interfaces;
using TaxCalculator.Services;
using TaxCalculator.API.Filters;
using TaxCalculator.Repositories;
using TaxCalculator.API.Authentication;

namespace TaxCalculator.API.IoC;

public static class NativeInjector {
    public static void ConfigureNativeInjector(this IServiceCollection services, IConfiguration configuration) {
        // Services
        services.AddScoped<ITaxCalculatorService, TaxCalculatorService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IUserService, UserService>();
        // Repositories
        services.AddScoped<ITaxCalculatorRepository, TaxCalculatorRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        // Filters
        services.AddScoped<JWTAuthenticationFilter>();
        // Decorators
        services.Decorate<ITaxCalculatorRepository, TaxCalculatorCache>();
    }
}

