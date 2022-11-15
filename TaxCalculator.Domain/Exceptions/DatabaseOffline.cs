namespace TaxCalculator.Exceptions;

public class DatabaseOfflineError : TaxAPIException {
    public DatabaseOfflineError() : base() { }
    public DatabaseOfflineError(string errorMessage) : base(errorMessage) { }
    public DatabaseOfflineError(string errorMessage, string userMessage) : base(errorMessage, userMessage) { }
    public DatabaseOfflineError(string errorMessage, string userMessage, Exception e) : base(errorMessage, userMessage, e) { }
}
