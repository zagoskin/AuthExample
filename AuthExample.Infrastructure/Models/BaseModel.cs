namespace AuthExample.Infrastructure.Models;
public class BaseModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
}
