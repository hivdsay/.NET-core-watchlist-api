using WatchList.Core.Tools.Concrete.Dto.UserWatchList.Request;
using WatchList.Core.Tools.Concrete.Results;
using WatchList.Entities.Concrete;

namespace WatchList.Business.Abstract.App;

public interface IUserWatchListService
{
    Task<BaseResponseModel> CreateUserWatchListAsync(CreateUserWatchListDto createUserWatchListDto);
    Task<BaseResponseModel> UpdateUserWatchListAsync(UpdateUserWatchListDto updateUserWatchListDto);
    Task<BaseResponseModel> DeleteUserWatchListAsync(int userWatchListId);
    Task<BaseResponseModel> GetAllUserWatchListsAsync();
    Task<BaseResponseModel> GetUserWatchListByIdAsync(int userWatchListId);
}