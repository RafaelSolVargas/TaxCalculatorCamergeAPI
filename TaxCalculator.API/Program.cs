using TaxCalculator.API.IoC;
using TaxCalculator.Domain.Configurations;
using TaxCalculator.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Configure the Settings, an Singleton class to contain all the configuration settings
var settings = Settings.GetInstance();
settings.setSettings(builder.Configuration);

// Configure the API port
builder.WebHost.UseUrls($"http://*:{settings.ApiPort}");

builder.Services.AddControllers();
// Configure the Swagger
builder.Services.AddSwagger();
builder.Services.AddEndpointsApiExplorer();

// Register the dependency services, repositories and filters
builder.Services.ConfigureNativeInjector(builder.Configuration);
builder.Services.AddHttpContextAccessor();

// Register the Memory Cache
builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the global error handler
app.UseExceptionHandler("/error");

// Finish configuring the app
app.UseRouting();
app.UseCors(x => x
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin());

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseEndpoints(endpoints => endpoints.MapControllers());
app.UseSwagger();
app.UseSwaggerUI();
app.Run();
