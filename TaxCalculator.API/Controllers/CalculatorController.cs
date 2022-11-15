using Microsoft.AspNetCore.Mvc;
using TaxCalculator.API.Filters;
using TaxCalculator.Services.Interfaces;

namespace TaxCalculator.Controllers;

[ApiController]
[Route("")]
public class CalculatorController : ControllerBase {
    private ITaxCalculatorService calculatorService;

    public CalculatorController(ITaxCalculatorService calculatorService) {
        this.calculatorService = calculatorService;
    }

    [HttpGet("calculajuros")]
    [ServiceFilter(typeof(JWTAuthenticationFilter))]
    public async Task<double> CalculateTax([FromQuery] double valorinicial, [FromQuery] int meses) {
        return await this.calculatorService.CalculateTaxAsync(valorinicial, meses);
    }
}
