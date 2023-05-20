namespace AuthExample.Domain.Responses;
public class AuthResponse
{
    public required UserResponse User { get; init; } = null!;
    public required string Token { get; init; } = null!;
}
