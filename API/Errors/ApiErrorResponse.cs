namespace API.Errors;

public record ApiErrorResponse
{
    public int StatusCode { get; init; }
    public string Message { get; init; }
    public string? Details { get; init; }
}
