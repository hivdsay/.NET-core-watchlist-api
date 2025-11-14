using WatchList.Core.Tools.Concrete.Dto.User.Request;
using WatchList.Core.Tools.Concrete.Dto.UserRole.Request;
using WatchList.Core.Tools.Concrete.Results;

namespace WatchList.Business.Abstract.App;

public interface IUserRoleService
{
    Task<BaseResponseModel> CreateUserRoleAsync(CreateUserRoleDto createUserRoleDto);
    Task<BaseResponseModel> UpdateUserRoleAsync(UpdateUserRoleDto updateUserRoleDto);
    Task<BaseResponseModel> DeleteUserRoleAsync(int userRoleId);
    Task<BaseResponseModel> GetAllUserRolesAsync();
    Task<BaseResponseModel> GetUserRoleByIdAsync(int userRoleId);
}