using TaxCalculator.Controllers;
using TaxCalculator.DTOs.Responses;
using TaxCalculator.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TaxCalculator.API.Authorization;
using TaxCalculator.Authorization;
using FileServerAPI.API.Filters;
using TaxCalculator.Services.Interfaces;

namespace TaxCalculator.API.Filters;
public class AuthorizationFilter : ActionFilterAttribute {
    private RoleAction action;

    private IUserService userService;

    public AuthorizationFilter(IUserService userService, RoleAction action) {
        this.userService = userService;
        this.action = action;
    }

    public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) {
        try {
            // Read the requestUser property, set by the AuthenticationFilter
            var controller = (BaseController)context.Controller;

            if (controller.requestUser == null || controller.requestUser.id == null) {
                this.SetForbiddenResult("User not found for authorization", context);
                return;
            }

            var userID = controller.requestUser!.id!;

            // Verify if got the permission to execute the action
            var authorizationFilter = this.GetFilterForAction(action, context, (int)userID);
            var authorized = await authorizationFilter.Execute();

            // If got the permission let the request continue, else return 403
            if (authorized)
                await next();
            else
                this.SetForbiddenResult("Forbidden", context);
        } catch (Exception error) {
            this.SetForbiddenResult(error.Message, context);
        }
    }

    private AbstractAuthoFilter GetFilterForAction(RoleAction action, ActionExecutingContext context, int userID) {
        Type filterType;
        this.actionToFilter.TryGetValue(action, out filterType!);

        if (filterType == null)
            throw new InternalError($"Filter for Action {action} has not been implemented");

        // Get the specific constructor
        Type[] argumentsTypes = new Type[] { typeof(IUserService), typeof(RoleAction),
            typeof(ActionExecutingContext), typeof(int) };
        var constructor = filterType.GetConstructor(argumentsTypes);

        if (constructor == null)
            throw new InternalError($"Constructor for filter type not found");

        // Execute the filter constructor
        var filterInstance = (AbstractAuthoFilter)constructor!.Invoke(new object[] { this.userService,
                        action, context, userID });


        return filterInstance;
    }

    private Dictionary<RoleAction, Type> actionToFilter = new Dictionary<RoleAction, Type>() {
        { RoleAction.CalculateTax, typeof(CanCalculateTaxFilter) }
    };

    private void SetForbiddenResult(string error, ActionExecutingContext context) {
        var result = new ObjectResult(new BaseAPIResponse(error)) { StatusCode = 403 };
        context.Result = result;
    }
}

