using Microsoft.EntityFrameworkCore;
using WatchList.Core.WatchListBaseEntity.Concrete;
using WatchList.DataAccess.Abstract.App;
using WatchList.DataAccess.Concrete.AppContext;
using WatchList.Entities.Concrete;

namespace WatchList.DataAccess.Concrete.Repository;

public class RoleRepository : BaseRepository<Role>, IRoleDal
{
    private WatchListDbContext dbCon => _context as WatchListDbContext;
    public RoleRepository(DbContext context) : base(context)
    {
    }
}