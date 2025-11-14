using AutoMapper;
using WatchList.Business.Abstract.App;
using WatchList.Business.Abstract.Generic;
using WatchList.Business.Concrete.App;
using WatchList.DataAccess.Abstract.UnitOfWorkApp;

namespace WatchList.Business.Concrete.Generic;

public class GenericManagerApp : ManagerBase, IGenericServiceApp
{
    public GenericManagerApp(IUnitOfWorkApp UnitOfWorkApp, IMapper IMapper) : base(UnitOfWorkApp, IMapper)
    {
    }

     MovieManager _movieManager;
     ReviewManager _reviewManager;
     UserManager _userManager;
     UserWatchListManager _userWatchListManager;
     UserRoleManager _userRoleManager;
     RoleManager _roleManager;
     
     public IMovieService MovieService => _movieManager ??= new MovieManager(_UnitOfWorkApp, _IMapper);
     public IReviewService ReviewService => _reviewManager ??= new ReviewManager(_UnitOfWorkApp, _IMapper);
     public IUserService UserService => _userManager ??= new UserManager(_UnitOfWorkApp, _IMapper);
     public IUserWatchListService UserWatchListService => _userWatchListManager ??= new UserWatchListManager(_UnitOfWorkApp, _IMapper);
     public IUserRoleService UserRoleService => _userRoleManager ??= new UserRoleManager(_UnitOfWorkApp, _IMapper);
     public IRoleService RoleService => _roleManager ??= new RoleManager(_UnitOfWorkApp, _IMapper);
}