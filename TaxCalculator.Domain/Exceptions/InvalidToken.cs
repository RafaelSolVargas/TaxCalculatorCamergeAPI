namespace TaxCalculator.Exceptions;

public class InvalidToken : TaxAPIException {
    public InvalidToken() : base() { }
    public InvalidToken(string errorMessage) : base(errorMessage) { }
    public InvalidToken(string errorMessage, string userMessage) : base(errorMessage, userMessage) { }
    public InvalidToken(string errorMessage, string userMessage, Exception e) : base(errorMessage, userMessage, e) { }
}
