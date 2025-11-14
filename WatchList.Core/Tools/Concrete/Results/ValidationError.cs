namespace WatchList.Core.Tools.Concrete.Results;

public class ValidationError
{
    public string PropertyName { get;}
    public string ErrorMessage { get;}

    public ValidationError(string propertyName, string errorMessage)
    {
        PropertyName = propertyName ?? throw new ArgumentNullException(nameof(propertyName));
        ErrorMessage = errorMessage ?? throw new ArgumentNullException(nameof(errorMessage));
    }
}