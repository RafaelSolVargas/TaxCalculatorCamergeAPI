using Microsoft.AspNetCore.Mvc.Filters;
using TaxCalculator.API.Authorization;
using TaxCalculator.Services.Interfaces;

namespace TaxCalculator.Authorization;
public abstract class AbstractAuthoFilter {
    protected IUserService userService;

    protected RoleAction action;

    protected ActionExecutingContext context;

    protected int userID;

    public AbstractAuthoFilter(IUserService userService, RoleAction action, ActionExecutingContext context, int userID) {
        this.action = action;
        this.context = context;
        this.userID = userID;
        this.userService = userService;
    }

    public abstract Task<bool> Execute();
}
