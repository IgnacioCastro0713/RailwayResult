using RailwayResult.Errors;

namespace RailwayResult.Results;

public interface IResult
{
    bool IsSuccess { get; }
    bool IsFailure { get; }
    IReadOnlyList<Error> Errors { get; }
}
