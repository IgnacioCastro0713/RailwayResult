namespace RailwayResult.Errors;

public class Error(string code, string message)
{
    public string Code { get; } = code;
    public string Message { get; } = message;

    public static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error Unknown = new("Unknown", "An unknown error occurred.");

    public override string ToString() => $"[{Code}] {Message}";
}
