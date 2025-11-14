using WatchList.Entities.Concrete;

namespace WatchList.Core.Tools.Concrete.Dto.UserRole.Request;

public class CreateUserRoleDto
{
    public int RoleId { get; set; }
    public int UserId { get; set; }
}