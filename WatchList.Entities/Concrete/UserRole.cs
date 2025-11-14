using WatchList.Entities.Concrete.Base;

namespace WatchList.Entities.Concrete;

public class UserRole : BaseEntity
{
    public int RoleId { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public Role Role { get; set; }
}