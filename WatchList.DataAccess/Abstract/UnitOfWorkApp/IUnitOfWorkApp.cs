using WatchList.DataAccess.Abstract.App;

namespace WatchList.DataAccess.Abstract.UnitOfWorkApp;

public interface IUnitOfWorkApp : IAsyncDisposable
{
    IMovieDal MovieDal { get; }
    IUserDal UserDal { get; }
    IReviewDal ReviewDal { get; }
    IUserWatchListDal UserWatchListDal { get; }
    IUserRoleDal UserRoleDal { get; }
    IRoleDal RoleDal { get; }
    
    Task<int> SaveAsync();
}