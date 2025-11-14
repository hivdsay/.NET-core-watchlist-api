using Microsoft.EntityFrameworkCore;
using WatchList.Core.WatchListBaseEntity.Abstract;
using WatchList.Core.WatchListBaseEntity.Concrete;
using WatchList.DataAccess.Abstract.App;
using WatchList.DataAccess.Concrete.AppContext;
using WatchList.Entities.Concrete;

namespace WatchList.DataAccess.Concrete.Repository;

public class MovieRepository : BaseRepository<Movie>, IMovieDal
{
    private WatchListDbContext dbCon => _context as WatchListDbContext;
    public MovieRepository(DbContext context) : base(context)
    {
        
    }
}