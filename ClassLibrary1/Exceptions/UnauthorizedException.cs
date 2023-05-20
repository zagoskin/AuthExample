namespace AuthExample.Domain.Exceptions;
public class UnauthorizedException : Exception
{
    public UnauthorizedException() : base("Unauthorized user")
    {
    }
}

