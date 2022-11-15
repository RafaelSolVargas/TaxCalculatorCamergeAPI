using Microsoft.AspNetCore.Mvc.Filters;
using TaxCalculator.API.Authorization;
using TaxCalculator.Authorization;
using TaxCalculator.Services.Interfaces;

namespace FileServerAPI.API.Filters;
public class CanCalculateTaxFilter : AbstractAuthoFilter {
    public CanCalculateTaxFilter(IUserService userService, RoleAction action, ActionExecutingContext context, int userID) : base(userService, action, context, userID) { }

    public override Task<bool> Execute() {
        var user = this.userService.Get(this.userID);
        return Task.FromResult(user.role.canCalculateTax);
    }
}
