using TaxCalculator.DTOs.Responses;
using TaxCalculator.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace TaxCalculator.Controllers;
// Using the error handler as a controller allow us to use dependency injection
// To forbidden the Asp.Net Code to log errors that are handled by this controller the line:
// "Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware": "None" was inserted in app.settings
// https://stackoverflow.com/questions/38630076/asp-net-core-web-api-exception-handling

[AllowAnonymous]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorsController : BaseController {
    [Route("error")]
    public BaseAPIResponse Error() {
        var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
        var exception = context!.Error;

        var error = exception.InnerException != null ? exception.InnerException : exception;
        var information = this.GetErrorInformation(error);

        Response.StatusCode = information.Item2;
        return new BaseAPIResponse(errorMessage: information.Item1, internalError: error.Message);
    }

    private Tuple<string, int> GetErrorInformation(Exception error) {
        if (!(error is TaxAPIException)) {
            Console.WriteLine("Not Tax API error triggered: ");
            Console.WriteLine(error.GetType());
            Console.WriteLine(error.Message);
            Console.WriteLine(error.StackTrace);

            return new Tuple<string, int>("Internal Error", StatusCodes.Status500InternalServerError);
        }

        var errorTaxAPI = (TaxAPIException)error;

        this.LogError(errorTaxAPI);
        var message = this.GetErrorMessage(errorTaxAPI);
        var code = this.GetErrorStatusCode(errorTaxAPI);

        return new Tuple<string, int>(message, code);
    }

    private void LogError(TaxAPIException error) {
        if ((error is InternalError) || (error is DatabaseOfflineError)) {
            Console.WriteLine("Error Occurred:");
            Console.WriteLine(error.errorMessage);
            Console.WriteLine(error.StackTrace);
        }
    }

    private string GetErrorMessage(TaxAPIException error) {
        if (error.userMessageDefined)
            return error.userMessage!;

        if (error is InvalidToken)
            return "No token found";
        if (error is InternalError)
            return "Internal Error";
        if (error is DatabaseOfflineError)
            return "The service was unable to connect to the database";
        if (error is ForbiddenError)
            return "Forbidden Action";
        else
            return "Internal Error";
    }

    private int GetErrorStatusCode(TaxAPIException error) {
        if (error is InvalidToken)
            return StatusCodes.Status401Unauthorized;
        if (error is InternalError)
            return StatusCodes.Status500InternalServerError;
        if (error is DatabaseOfflineError)
            return StatusCodes.Status500InternalServerError;
        if (error is ForbiddenError)
            return StatusCodes.Status403Forbidden;
        else
            return StatusCodes.Status500InternalServerError;
    }
}
