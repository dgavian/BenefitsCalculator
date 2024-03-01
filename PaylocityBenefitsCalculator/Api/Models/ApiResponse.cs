namespace Api.Models;

// TODO: Get rid of this and make it more RESTful (or not if it's needed to make the integration tests pass).
public class ApiResponse<T>
{
    public T? Data { get; set; }
    public bool Success { get; set; } = true;
    public string Message { get; set; } = string.Empty;
    public string Error { get; set; } = string.Empty;
}
