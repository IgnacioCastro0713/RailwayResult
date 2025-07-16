using RailwayResult.Errors;

namespace RailwayResult.Results;

public class Result : IResult
{
    public bool IsSuccess { get; }
    public bool IsFailure { get; }
    public IReadOnlyList<Error> Errors { get; }

    protected Result(bool isSuccess, List<Error> errors)
    {
        switch (isSuccess)
        {
            case true when errors.Any(e => e != Error.None):
                throw new InvalidOperationException("Successful result cannot have errors other than Error.None.");
            case false when !errors.Any():
                throw new InvalidOperationException("Failed result must have at least one error.");
        }

        IsSuccess = isSuccess;
        IsFailure = !isSuccess;
        Errors = errors.AsReadOnly();
    }

    public static Result Success() => new Result(true, [Error.None]);
    public static Result Failure(Error error) => new(false, [error]);
    public static Result Failure(IEnumerable<Error> errors) => new Result(false, errors.ToList());
    public static Result<T> Success<T>(T value) => Result<T>.Success(value);
    public static Result<T> Failure<T>(Error error) => Result<T>.Failure(error);
    public static Result<T> Failure<T>(IEnumerable<Error> errors) => Result<T>.Failure(errors);

    public static Result<T> Of<T>(T value) => Result<T>.Success(value);
    public static Result<T> Of<T>(Func<T> func) => Result<T>.Success(func());
    public static async Task<Result<T>> Of<T>(Func<Task<T>> func) => Result<T>.Success(await func());

    public static Result Combine(params IResult[] results)
    {
        var errors = results.Where(r => r.IsFailure).SelectMany(r => r.Errors).ToList();
        return errors.Any() ? Failure(errors) : Success();
    }

    public string GetCombinedErrorMessage()
    {
        return IsSuccess
            ? string.Empty
            : string.Join("; ", Errors.Select(e => $"{e.Code}: {e.Message}"));
    }
}
