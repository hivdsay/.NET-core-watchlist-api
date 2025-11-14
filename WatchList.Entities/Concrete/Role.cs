using WatchList.Entities.Concrete.Base;


namespace WatchList.Entities.Concrete;

public class Role : BaseEntity
{
    public string RoleName { get; set; }
    public string Description { get; set; }
    public List<UserRole> UserRoles { get; set; } = new List<UserRole>();
}