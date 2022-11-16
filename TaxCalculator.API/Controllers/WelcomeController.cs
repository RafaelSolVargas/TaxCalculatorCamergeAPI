using Microsoft.AspNetCore.Mvc;

namespace TaxCalculator.Controllers;

[ApiController]
[Route("")]
public class WelcomeController : BaseController {
    [HttpGet]
    [Route("")]
    [ProducesResponseType(typeof(string), 200)]
    public ActionResult WelcomePage() {
        return StatusCode(StatusCodes.Status200OK, "Welcome to Tax API.");
    }

    [HttpGet]
    [Route("showmethecode")]
    [ProducesResponseType(typeof(string), 200)]
    public ActionResult ShowMeTheCode() {
        return StatusCode(StatusCodes.Status200OK, "https://github.com/RafaelSolVargas/TaxCalculatorCamergeAPI");
    }
}
