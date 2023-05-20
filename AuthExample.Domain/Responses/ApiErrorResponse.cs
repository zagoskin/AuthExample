namespace AuthExample.Domain.Responses;
public class ApiErrorResponse
{
    public int StatusCode { get; set; } = 500;
    public List<string> Errors { get; set; } = new();
}
