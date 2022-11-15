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
}
