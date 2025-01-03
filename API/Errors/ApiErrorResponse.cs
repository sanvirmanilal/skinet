namespace API.Errors;

public record ApiErrorResponse
{
    public int StatusCode { get; init; }
    public string Message { get; init; } = string.Empty;
    public string? Details { get; init; }
}
