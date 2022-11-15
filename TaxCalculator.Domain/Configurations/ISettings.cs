using Microsoft.Extensions.Configuration;
using TaxCalculator.Exceptions;

namespace TaxCalculator.Domain.Configurations;
public class Settings {
    private static readonly object _lock = new object();

    private static Settings? _instance;

    public JwtOptions? jwtOptions;

    public string? ApiPort;

    public string? taxApiURL;

    public void setSettings(IConfiguration settings) {
        this.ApiPort = settings[nameof(this.ApiPort)];
        this.taxApiURL = settings[nameof(this.taxApiURL)];
        this.jwtOptions = settings.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

        if (!this.verifyAllValuesLoaded())
            throw new InternalError("AppSettings.json was not loaded successfully and the API cannot start.");

        // Jwt Configuration -> Allow using environment variables to override the values in appsettings
        var env_JwtSecretKey = Environment.GetEnvironmentVariable(nameof(jwtOptions.JwtSecretKey), EnvironmentVariableTarget.Process);
        if (env_JwtSecretKey != null)
            this.jwtOptions.JwtSecretKey = env_JwtSecretKey;
    }

    public static Settings GetInstance() {
        if (_instance == null) {
            lock (_lock) {

                if (_instance == null) {
                    _instance = new Settings();
                }
            }
        }
        return _instance;
    }

    private bool verifyAllValuesLoaded() {
        if (this.jwtOptions == null || this.ApiPort == null || this.taxApiURL == null)
            return false;

        var jwtProperties = this.jwtOptions!.GetType().GetProperties().ToList();

        foreach (var property in jwtProperties) {
            var propertyValue = property.GetValue(this.jwtOptions);

            if (propertyValue == null)
                return false;

            if ((property.PropertyType == typeof(int) || property.PropertyType == typeof(float)) && (int)propertyValue == 0)
                return false;
        }

        return true;
    }
}

