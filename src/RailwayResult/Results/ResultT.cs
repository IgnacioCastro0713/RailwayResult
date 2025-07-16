using RailwayResult.Errors;

namespace RailwayResult.Results;

public class Result<T> : Result
{
    private readonly T _value;

    public T Value
    {
        get
        {
            if (!IsSuccess)
            {
                throw new InvalidOperationException("Cannot access value for a failed result.");
            }

            return _value;
        }
    }

    private Result(T value, bool isSuccess, List<Error> errors) : base(isSuccess, errors) => _value = value;

    public static Result<T> Success(T value) => new(value, true, [Error.None]);
    public static new Result<T> Failure(Error error) => new(default!, false, [error]);
    public static new Result<T> Failure(IEnumerable<Error> errors) => new(default!, false, errors.ToList());

    public static implicit operator Result<T>(T value) => new(value, true, [Error.None]);
}
