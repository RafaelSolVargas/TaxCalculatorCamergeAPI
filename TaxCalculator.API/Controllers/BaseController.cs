using Microsoft.AspNetCore.Mvc;
using TaxCalculator.Domain.Entities;

namespace TaxCalculator.Controllers;

[ApiController]
[Route("")]
public class BaseController : ControllerBase {
    // The user that send the request, is set by the Authentication Filter
    public User? requestUser { get; set; }
}
