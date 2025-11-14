using System.Net;
using AutoMapper;
using WatchList.Business.Abstract.App;
using WatchList.Business.Concrete.Generic;
using WatchList.Core.Tools.Concrete.Dto.UserRole.Request;
using WatchList.Core.Tools.Concrete.Dto.UserRole.Response;
using WatchList.Core.Tools.Concrete.Results;
using WatchList.DataAccess.Abstract.UnitOfWorkApp;
using WatchList.Entities.Concrete;

namespace WatchList.Business.Concrete.App;

public class UserRoleManager : ManagerBase, IUserRoleService
{
    public UserRoleManager(IUnitOfWorkApp unitOfWorkApp, IMapper IMapper) : base(unitOfWorkApp, IMapper)
    {
    }

    public async Task<BaseResponseModel> CreateUserRoleAsync(CreateUserRoleDto createUserRoleDto)
    {
        // Check if user exists
        var user = await _UnitOfWorkApp.UserDal.GetByIdAsync(createUserRoleDto.UserId);
        if (user == null)
        {
            return new BaseResponseModel
            {
                StatusCode = HttpStatusCode.NotFound,
                Data = false,
                Description = "User not found"
            };
        }

        // Check if role exists
        var role = await _UnitOfWorkApp.RoleDal.GetByIdAsync(createUserRoleDto.RoleId);
        if (role == null)
        {
            return new BaseResponseModel
            {
                StatusCode = HttpStatusCode.NotFound,
                Data = false,
                Description = "Role not found"
            };
        }

        // Check if user-role relationship already exists
        var existingUserRole = await _UnitOfWorkApp.UserRoleDal.GetAsync(ur =>
            ur.UserId == createUserRoleDto.UserId && ur.RoleId == createUserRoleDto.RoleId);

        if (existingUserRole != null)
        {
            return new BaseResponseModel
            {
                StatusCode = HttpStatusCode.Conflict,
                Data = false,
                Description = "This user-role relationship already exists"
            };
        }

        var userRole = _IMapper.Map<UserRole>(createUserRoleDto);
        await _UnitOfWorkApp.UserRoleDal.AddAsync(userRole);
        await _UnitOfWorkApp.SaveAsync();

        return new BaseResponseModel
        {
            StatusCode = HttpStatusCode.Created,
            Data = true,
            Description = "User role assigned successfully"
        };
    }

    public async Task<BaseResponseModel> UpdateUserRoleAsync(UpdateUserRoleDto updateUserRoleDto)
    {
        var existingUserRole = await _UnitOfWorkApp.UserRoleDal.GetByIdAsync(updateUserRoleDto.Id);
        if (existingUserRole == null)
        {
            return new BaseResponseModel
            {
                StatusCode = HttpStatusCode.NotFound,
                Data = false,
                Description = "User role relationship not found"
            };
        }

        // Check if user exists
        var user = await _UnitOfWorkApp.UserDal.GetByIdAsync(updateUserRoleDto.UserId);
        if (user == null)
        {
            return new BaseResponseModel
            {
                StatusCode = HttpStatusCode.NotFound,
                Data = false,
                Description = "User not found"
            };
        }

        // Check if role exists
        var role = await _UnitOfWorkApp.RoleDal.GetByIdAsync(updateUserRoleDto.RoleId);
        if (role == null)
        {
            return new BaseResponseModel
            {
                StatusCode = HttpStatusCode.NotFound,
                Data = false,
                Description = "Role not found"
            };
        }

        // Check if another user-role relationship with same user and role exists (excluding current one)
        var duplicateUserRole = await _UnitOfWorkApp.UserRoleDal.GetAsync(ur =>
            ur.UserId == updateUserRoleDto.UserId && 
            ur.RoleId == updateUserRoleDto.RoleId && 
            ur.Id != updateUserRoleDto.Id);

        if (duplicateUserRole != null)
        {
            return new BaseResponseModel
            {
                StatusCode = HttpStatusCode.Conflict,
                Data = false,
                Description = "This user-role relationship already exists"
            };
        }

        _IMapper.Map(updateUserRoleDto, existingUserRole);
        await _UnitOfWorkApp.UserRoleDal.UpdateAsync(existingUserRole);
        await _UnitOfWorkApp.SaveAsync();

        return new BaseResponseModel
        {
            StatusCode = HttpStatusCode.OK,
            Data = true,
            Description = "User role updated successfully"
        };
    }

    public async Task<BaseResponseModel> DeleteUserRoleAsync(int userRoleId)
    {
        var userRole = await _UnitOfWorkApp.UserRoleDal.GetByIdAsync(userRoleId);
        if (userRole == null)
        {
            return new BaseResponseModel
            {
                StatusCode = HttpStatusCode.NotFound,
                Description = "User role relationship not found"
            };
        }

        await _UnitOfWorkApp.UserRoleDal.DeleteAsync(userRole);
        await _UnitOfWorkApp.SaveAsync();

        return new BaseResponseModel
        {
            StatusCode = HttpStatusCode.OK,
            Description = "User role relationship deleted successfully"
        };
    }

    public async Task<BaseResponseModel> GetAllUserRolesAsync()
    {
        var userRoles = await _UnitOfWorkApp.UserRoleDal.GetAllAsync(ur => ur.IsActive);

        if (userRoles == null || userRoles.Count == 0)
        {
            return new BaseResponseModel
            {
                StatusCode = HttpStatusCode.NotFound,
                Description = "No user roles found",
                Data = new List<UserRoleResponse>()
            };
        }

        var userRoleDtos = _IMapper.Map<List<UserRoleResponse>>(userRoles);

        return new BaseResponseModel
        {
            StatusCode = HttpStatusCode.OK,
            Description = "User roles retrieved successfully",
            Data = userRoleDtos
        };
    }

    public async Task<BaseResponseModel> GetUserRoleByIdAsync(int userRoleId)
    {
        var userRole = await _UnitOfWorkApp.UserRoleDal.GetByIdAsync(userRoleId);

        if (userRole == null)
        {
            return new BaseResponseModel
            {
                StatusCode = HttpStatusCode.NotFound,
                Description = "User role relationship not found"
            };
        }

        var userRoleDto = _IMapper.Map<UserRoleResponse>(userRole);

        return new BaseResponseModel
        {
            StatusCode = HttpStatusCode.OK,
            Description = "User role retrieved successfully",
            Data = userRoleDto
        };
    }

    public async Task<BaseResponseModel> GetUserRolesByUserIdAsync(int userId)
    {
        var user = await _UnitOfWorkApp.UserDal.GetByIdAsync(userId);
        if (user == null)
        {
            return new BaseResponseModel
            {
                StatusCode = HttpStatusCode.NotFound,
                Data = false,
                Description = "User not found"
            };
        }

        var userRoles = await _UnitOfWorkApp.UserRoleDal.GetAllAsync(
            ur => ur.UserId == userId && ur.IsActive);

        var userRoleDtos = _IMapper.Map<List<UserRoleResponse>>(userRoles);

        return new BaseResponseModel
        {
            StatusCode = HttpStatusCode.OK,
            Description = "User roles retrieved successfully",
            Data = userRoleDtos
        };
    }

    public async Task<BaseResponseModel> GetUsersByRoleIdAsync(int roleId)
    {
        var role = await _UnitOfWorkApp.RoleDal.GetByIdAsync(roleId);
        if (role == null)
        {
            return new BaseResponseModel
            {
                StatusCode = HttpStatusCode.NotFound,
                Data = false,
                Description = "Role not found"
            };
        }

        var userRoles = await _UnitOfWorkApp.UserRoleDal.GetAllAsync(
            ur => ur.RoleId == roleId && ur.IsActive);

        var userRoleDtos = _IMapper.Map<List<UserRoleResponse>>(userRoles);

        return new BaseResponseModel
        {
            StatusCode = HttpStatusCode.OK,
            Description = "Users with role retrieved successfully",
            Data = userRoleDtos
        };
    }
}