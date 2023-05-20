namespace AuthExample.Domain.Exceptions;
public class NotFoundException : Exception
{
    public NotFoundException(string entityName, string entityQuery) : base($"{entityName} '{entityQuery}' not found.")
    {
    }
    public NotFoundException(string entityName, Guid entityId) : base($"{entityName} '{entityId}' not found.")
    {
    }
}
