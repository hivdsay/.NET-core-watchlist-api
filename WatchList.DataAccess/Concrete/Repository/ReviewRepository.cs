using Microsoft.EntityFrameworkCore;
using WatchList.Core.WatchListBaseEntity.Concrete;
using WatchList.DataAccess.Abstract.App;
using WatchList.DataAccess.Concrete.AppContext;
using WatchList.Entities.Concrete;

namespace WatchList.DataAccess.Concrete.Repository;

public class ReviewRepository : BaseRepository<Review>, IReviewDal
{
    private WatchListDbContext dbCon => _context as WatchListDbContext;
    
    public ReviewRepository(DbContext context) : base(context)
    {
        
    }
}