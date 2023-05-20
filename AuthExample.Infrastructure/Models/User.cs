namespace AuthExample.Infrastructure.Models;
public class User : BaseModel
{
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public required byte[] PasswordHash { get; set; } = null!;
    public required byte[] PasswordSalt { get; init; } = null!;
    
    public Guid? RoleId { get; set; }

    // só poderia ter um... senão seria 
    // public virtual ICollection<Role> Roles { get; set; }   
    // e teria que ter uma tabela de relacionamento UserHasRole com os dois Ids
    public virtual Role? Role { get; set; } 
}
