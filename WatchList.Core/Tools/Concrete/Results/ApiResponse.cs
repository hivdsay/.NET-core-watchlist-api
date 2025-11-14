using WatchList.Core.Tools.Concrete.Results;

namespace WatchList.Core.Tools.Concrete.Results;

public class ApiResponse
{
    const string Validation = "VALIDATION"; 
    public string? RequestId { get; set; }
    public string? Error  { get; set; }
    public string? ErrorMessage { get; set; }
    public List<ValidationError>? ValidationErrors { get; set; }
    public object? Result { get; set; }

    public ApiResponse(string? requestId = null, object? result = null)
    {
        RequestId = requestId;
        Result = result;
    }

    public ApiResponse(string? requestId, string? errorMessage, List<ValidationError>? validationErrors)
    {
        RequestId = requestId;
        Error = Validation;
        ErrorMessage = errorMessage;
        ValidationErrors = validationErrors;
    }

    public ApiResponse(string? requestId = null, string? error = null, string? errorMessage = null,
        List<ValidationError>? validationErrors = null)
    {
        RequestId = requestId;
        Error = error;
        ErrorMessage = errorMessage;
        ValidationErrors = validationErrors;
    }
}

public class ApiResponse<T>
{
    public string? RequestId { get; set; }
    public string? Error { get; set; }
    public string? ErrorMessage { get; set; }
    public List<ValidationError>? ValidationErrors { get; set; }
    public T? Result { get; set; }
}