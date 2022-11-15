namespace TaxCalculator.Exceptions;

public class TaxAPIException : Exception {
    public string? userMessage { get; set; }

    public string? errorMessage { get; set; }

    public bool userMessageDefined => userMessage != null;

    public TaxAPIException() : base() { }

    public TaxAPIException(string errorMessage) : base(errorMessage) {
        this.errorMessage = errorMessage;
    }

    public TaxAPIException(string errorMessage, string userMessage) : base(errorMessage) {
        this.userMessage = userMessage;
        this.errorMessage = errorMessage;
    }

    public TaxAPIException(string errorMessage, string userMessage, Exception e) : base(errorMessage, e) {
        this.errorMessage = errorMessage;
        this.userMessage = userMessage;
    }
}
