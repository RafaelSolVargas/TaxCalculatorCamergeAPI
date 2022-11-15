using Microsoft.AspNetCore.Mvc;
using TaxCalculator.API.Authentication;
using TaxCalculator.API.Authorization;
using TaxCalculator.API.Filters;
using TaxCalculator.Services.Interfaces;

namespace TaxCalculator.Controllers;

[ApiController]
[Route("")]
public class CalculatorController : BaseController {
    private ITaxCalculatorService calculatorService;

    public CalculatorController(ITaxCalculatorService calculatorService) {
        this.calculatorService = calculatorService;
    }

    [HttpGet("calculajuros")]
    [ServiceFilter(typeof(JWTAuthenticationFilter))]
    [TypeFilter(typeof(AuthorizationFilter), Arguments = new object[] { RoleAction.CalculateTax })]
    public async Task<string> CalculateTax([FromQuery] double valorinicial, [FromQuery] int meses) {
        var result = await this.calculatorService.CalculateTaxAsync(valorinicial, meses);

        return result.ToString("F2");
    }
}
