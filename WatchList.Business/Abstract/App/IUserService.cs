using WatchList.Core.Tools.Concrete.Dto.User.Request;
using WatchList.Core.Tools.Concrete.Dto.User.Response;
using WatchList.Core.Tools.Concrete.Results;
using WatchList.Entities.Concrete;

namespace WatchList.Business.Abstract.App;

public interface IUserService
{
    Task<BaseResponseModel> CreateUserAsync(CreateUserDto createUserDto);
    Task<BaseResponseModel> UpdateUserAsync(UpdateUserDto updateUserDto);
    Task<BaseResponseModel> DeleteUserAsync(int userId);
    Task<BaseResponseModel> GetAllUsersAsync();
    Task<BaseResponseModel> GetUserByIdAsync(int userId);
    Task<BaseResponseModel> GetUserRoleAsync();
    Task<User> UserLoginCheck(LoginRequestDto request);
}