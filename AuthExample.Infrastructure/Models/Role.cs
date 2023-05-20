using AuthExample.Infrastructure.Enums;

namespace AuthExample.Infrastructure.Models;
public class Role : BaseModel
{
    public Role()
    {
        Users = new HashSet<User>();
    }
    public RoleType Name { get; set; }
    public string? Description { get; set; }
    public ICollection<User> Users { get; set; } 
}
