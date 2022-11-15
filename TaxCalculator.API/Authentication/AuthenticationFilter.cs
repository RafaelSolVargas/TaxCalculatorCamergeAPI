using TaxCalculator.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TaxCalculator.DTOs.Responses;
using TaxCalculator.Controllers;

namespace TaxCalculator.API.Authentication;
public class JWTAuthenticationFilter : ActionFilterAttribute {
    private ITokenService tokenService;

    public JWTAuthenticationFilter(ITokenService tokenService) {
        this.tokenService = tokenService;
    }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) {
        try {
            var controller = (BaseController)context.Controller;

            var authString = (string)context.HttpContext.Request.Headers.Authorization;
            if (authString == null) {
                this.SetUnauthorizedResult("Authentication Token not found", context);
                return;
            }

            var tokenInfo = authString.Split(' ');
            // Verify if token has two parts
            if (tokenInfo.Count() != 2 || tokenInfo[0] != "Bearer") {
                this.SetUnauthorizedResult($"JWT token is broken", context);
                return;
            }

            var token = tokenInfo[1];
            var user = this.tokenService.ValidateAccessToken(token)!;

            if (user == null) {
                this.SetUnauthorizedResult("User in Token not found", context);
                return;
            }

            controller.requestUser = user;
            await next();
        } catch (Exception error) {
            this.SetUnauthorizedResult(error.Message, context);
        }
    }

    private void SetUnauthorizedResult(string error, ActionExecutingContext context) {
        var result = new ObjectResult(new BaseAPIResponse(error)) { StatusCode = 401 };
        context.Result = result;
    }
}
