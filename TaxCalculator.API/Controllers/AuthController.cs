using Microsoft.AspNetCore.Mvc;
using TaxCalculator.Domain.DTOs;
using TaxCalculator.DTOs.Responses;
using TaxCalculator.Services.Interfaces;

namespace TaxCalculator.Controllers;

[ApiController]
[Route("")]
public class AuthController : BaseController {
    private IIdentityService identityService;
    private ITokenService tokenService;

    public AuthController(IIdentityService identityService, ITokenService tokenService) {
        this.identityService = identityService;
        this.tokenService = tokenService;
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponse), 200)]
    [ProducesResponseType(typeof(BaseAPIResponse), 400)]
    [ProducesResponseType(typeof(BaseAPIResponse), 401)]
    [ProducesResponseType(typeof(BaseAPIResponse), 500)]
    public ActionResult<LoginResponse> Login([FromBody] LoginRequest request) {
        var user = this.identityService.Login(request);

        if (user == null)
            return Unauthorized(new BaseAPIResponse("Invalid credentials"));

        // Generate JWT token
        var accessToken = this.tokenService.GenerateAccessToken(user);

        var response = new LoginResponse(accessToken, user);
        return StatusCode(StatusCodes.Status200OK, response);
    }
}
