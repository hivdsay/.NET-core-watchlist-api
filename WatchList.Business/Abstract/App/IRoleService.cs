using WatchList.Core.Tools.Concrete.Dto.Role.Request;
using WatchList.Core.Tools.Concrete.Dto.UserRole.Request;
using WatchList.Core.Tools.Concrete.Results;

namespace WatchList.Business.Abstract.App;

public interface IRoleService
{
    Task<BaseResponseModel> CreateRoleAsync(CreateRoleDto createRoleDto);
    Task<BaseResponseModel> UpdateRoleAsync(UpdateRoleDto updateRoleDto);
    Task<BaseResponseModel> DeleteRoleAsync(int roleId);
    Task<BaseResponseModel> GetAllRolesAsync();
    Task<BaseResponseModel> GetRoleByIdAsync(int roleId);
}