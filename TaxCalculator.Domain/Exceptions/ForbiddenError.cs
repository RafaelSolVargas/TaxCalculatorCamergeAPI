namespace TaxCalculator.Exceptions;

public class ForbiddenError : TaxAPIException {
    public ForbiddenError() : base() { }
    public ForbiddenError(string errorMessage) : base(errorMessage) { }
    public ForbiddenError(string errorMessage, string userMessage) : base(errorMessage, userMessage) { }
    public ForbiddenError(string errorMessage, string userMessage, Exception e) : base(errorMessage, userMessage, e) { }
}
