using WatchList.Core.Tools.Concrete.Dto.UserRole.Response;
using WatchList.Core.WatchListBaseEntity.Abstract;
using WatchList.Entities.Concrete;

namespace WatchList.DataAccess.Abstract.App;

public interface IUserDal : IBaseRepository<User>
{
    Task<List<UserRoleResponse>> GetUserRoleAsync();
}