namespace TaxCalculator.DTOs.Responses;
public class BaseAPIResponse {
    public List<string> errors { get; set; }

    public string? internalError { get; set; }

    public bool success => errors.Count == 0;

    public BaseAPIResponse() {
        this.errors = new List<string>();
    }

    public BaseAPIResponse(string? errorMessage) {
        this.errors = new List<string>();
        if (errorMessage != null)
            this.errors.Add(errorMessage);
    }

    public BaseAPIResponse(string? errorMessage, string? internalError) {
        this.errors = new List<string>();
        this.internalError = internalError;
        if (errorMessage != null)
            this.errors.Add(errorMessage);
    }

    public void AddError(string error) =>
        this.errors.Add(error);

    public void AddErrorsList(IEnumerable<string> errors) =>
        this.errors.AddRange(errors);
}
