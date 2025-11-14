using Microsoft.EntityFrameworkCore;
using WatchList.Core.Tools.Concrete.Dto.UserRole.Response;
using WatchList.Core.WatchListBaseEntity.Concrete;
using WatchList.DataAccess.Abstract.App;
using WatchList.DataAccess.Concrete.AppContext;
using WatchList.Entities.Concrete;

namespace WatchList.DataAccess.Concrete.Repository;

public class UserRepository : BaseRepository<User>, IUserDal
{
    private WatchListDbContext dbCon => _context as WatchListDbContext;
    public UserRepository(DbContext context) : base(context)
    {
    }


    public async Task<List<UserRoleResponse>> GetUserRoleAsync()
    {
        return await (from a in dbCon.Users.Where(x => x.IsActive && !x.IsDeleted)
            join b in dbCon.UserRole.Where(x => x.IsActive && !x.IsDeleted)
                on a.Id equals b.UserId
            join c in dbCon.Roles.Where(x => x.IsActive && !x.IsDeleted)
                on b.RoleId equals c.Id
            select new UserRoleResponse
            {
                RoleName = c.RoleName.ToString(),
                Email = a.Email
            }).AsNoTracking().ToListAsync();
    }
}