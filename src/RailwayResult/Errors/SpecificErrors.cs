namespace RailwayResult.Errors;

public class ValidationError(string propertyName, string message) : Error("Validation", message)
{
    public string PropertyName { get; } = propertyName;
}

public class NotFoundError(string resourceName, string message) : Error("NotFound", message)
{
    public string ResourceName { get; } = resourceName;
}

public class UnauthorizedError(string message) : Error("Unauthorized", message);

