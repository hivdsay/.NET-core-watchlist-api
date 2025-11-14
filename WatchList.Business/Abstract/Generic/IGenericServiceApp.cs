using WatchList.Business.Abstract.App;

namespace WatchList.Business.Abstract.Generic;

public interface IGenericServiceApp
{
    IMovieService  MovieService { get; }
    IUserService  UserService { get; }
    IReviewService  ReviewService { get; }
    IUserWatchListService  UserWatchListService { get; }
    IUserRoleService  UserRoleService { get; }
    IRoleService  RoleService { get; }
}