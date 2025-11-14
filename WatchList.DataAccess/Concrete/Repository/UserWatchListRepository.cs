using Microsoft.EntityFrameworkCore;
using WatchList.Core.WatchListBaseEntity.Concrete;
using WatchList.DataAccess.Abstract.App;
using WatchList.DataAccess.Concrete.AppContext;
using WatchList.Entities.Concrete;

namespace WatchList.DataAccess.Concrete.Repository;

public class UserWatchListRepository : BaseRepository<UserWatchList>, IUserWatchListDal
{
    // BaseRepository'deki _context'i WatchListDbContext olarak kullanabilmek için
    private WatchListDbContext dbCon => _context as WatchListDbContext; 
    public UserWatchListRepository(DbContext context) : base(context)
    {
    }
}