using Microsoft.AspNetCore.Mvc;

namespace TaxAPI.Controllers;

[ApiController]
[Route("")]
public class WelcomeController : ControllerBase {
    [HttpGet]
    [Route("")]
    public ActionResult WelcomePage() {
        return StatusCode(StatusCodes.Status200OK, "Welcome to Tax API.");
    }
}
