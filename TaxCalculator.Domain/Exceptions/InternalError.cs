namespace TaxCalculator.Exceptions;

public class InternalError : TaxAPIException {
    public InternalError() : base() { }
    public InternalError(string errorMessage) : base(errorMessage) { }
    public InternalError(string errorMessage, string userMessage) : base(errorMessage, userMessage) { }
    public InternalError(string errorMessage, string userMessage, Exception e) : base(errorMessage, userMessage, e) { }
}
