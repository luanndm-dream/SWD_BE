namespace BusDelivery.Contract.Abstractions.Shared;
public class Result
{
    protected internal Result(bool isSuccess, Error error, int? statusCode = 200)
    {
        if (isSuccess && error != Error.None)
        {
            throw new InvalidOperationException();
        }

        if (!isSuccess && error == Error.None)
        {
            throw new InvalidOperationException();
        }

        IsSuccess = isSuccess;
        message = error;
        StatusCode = statusCode;
    }

    public bool IsSuccess { get; }

    public Error message { get; }

    public int? StatusCode { get; } = 200;

    public static Result Success() => new(true, Error.None);

    public static Result<TValue> Success<TValue>(TValue value) =>
        new(value, true, Error.None);

    public static Result<TValue> Success<TValue>(TValue value, int statusCode) =>
        new(value, true, Error.None, statusCode);

    public static Result<TValue> Success<TValue>(int statusCode) =>
        new(default, true, Error.None, statusCode);
    public static Result Failure(Error error) =>
        new(false, error, 400);

    public static Result<TValue> Failure<TValue>(Error error) =>
        new(default, false, error);

    public static Result<TValue> Failure<TValue>(Error error, int statusCode) =>
    new(default, false, error, statusCode);

    public static Result<TValue> Create<TValue>(TValue? value) =>
        value is not null ? Success(value) : Failure<TValue>(Error.NullValue);


}

