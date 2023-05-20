namespace AuthExample.Domain.Exceptions;
public class BusinessRuleException : Exception
{
    public BusinessRuleException(string message) : base(message)
    {
        MessageErrors.Add(message);
    }
    public List<string> MessageErrors { get; set; } = new();
}

