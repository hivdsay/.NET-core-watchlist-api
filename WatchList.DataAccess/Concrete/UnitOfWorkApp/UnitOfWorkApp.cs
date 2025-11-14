using WatchList.DataAccess.Abstract.App;
using WatchList.DataAccess.Abstract.UnitOfWorkApp;
using WatchList.DataAccess.Concrete.AppContext;
using WatchList.DataAccess.Concrete.Repository;

namespace WatchList.DataAccess.Concrete.UnitOfWorkApp;

public class UnitOfWorkApp : IUnitOfWorkApp
{
    private readonly WatchListDbContext _context;

    public UnitOfWorkApp(WatchListDbContext context)
    {
        _context = context;
    }
    
    private MovieRepository _movieRepository;
    private UserRepository _userRepository;
    private ReviewRepository _reviewRepository;
    private UserWatchListRepository _userWatchListRepository;
    private UserRoleRepository _userRoleRepository;
    private RoleRepository _roleRepository;
    
    public IMovieDal MovieDal => _movieRepository ??= new MovieRepository(_context);
    public IUserDal UserDal => _userRepository ??= new UserRepository(_context);
    public IReviewDal ReviewDal => _reviewRepository ??= new ReviewRepository(_context);
    public IRoleDal RoleDal => _roleRepository ??= new RoleRepository(_context);
    public IUserRoleDal UserRoleDal => _userRoleRepository ??= new UserRoleRepository(_context);
    public IUserWatchListDal UserWatchListDal => _userWatchListRepository ??= new UserWatchListRepository(_context);
    public async ValueTask DisposeAsync() => await _context.DisposeAsync(); //Context’i serbest bırakır, memory leak önlenir.
    public async Task<int> SaveAsync() => await _context.SaveChangesAsync();
}