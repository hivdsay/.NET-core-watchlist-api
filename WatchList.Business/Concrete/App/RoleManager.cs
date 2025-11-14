using System.Net;
using AutoMapper;
using WatchList.Business.Abstract.App;
using WatchList.Business.Concrete.Generic;
using WatchList.Core.Tools.Concrete.Dto.Role.Request;
using WatchList.Core.Tools.Concrete.Dto.Role.Response;
using WatchList.Core.Tools.Concrete.Results;
using WatchList.DataAccess.Abstract.UnitOfWorkApp;
using WatchList.Entities.Concrete;

namespace WatchList.Business.Concrete.App;

public class RoleManager : ManagerBase, IRoleService
{
    public RoleManager(IUnitOfWorkApp UnitOfWorkApp, IMapper IMapper) : base(UnitOfWorkApp, IMapper)
    {
    }

    public async Task<BaseResponseModel> CreateRoleAsync(CreateRoleDto createRoleDto)
    {
        var existingRole = await _UnitOfWorkApp.RoleDal.GetAsync(r =>
            r.RoleName.ToLower() == createRoleDto.RoleName.ToLower());

        if (existingRole != null)
        {
            return new BaseResponseModel
            {
                StatusCode = HttpStatusCode.Conflict,
                Data = false,
                Description = $"A role with name '{createRoleDto.RoleName}' already exists"
            };
        }

        var role = _IMapper.Map<Role>(createRoleDto);
        await _UnitOfWorkApp.RoleDal.AddAsync(role);
        await _UnitOfWorkApp.SaveAsync();

        return new BaseResponseModel
        {
            StatusCode = HttpStatusCode.Created,
            Data = true,
            Description = "Role created successfully"
        };
    }

     public async Task<BaseResponseModel> UpdateRoleAsync(UpdateRoleDto updateRoleDto)
    {
        var existingRole = await _UnitOfWorkApp.RoleDal.GetByIdAsync(updateRoleDto.RoleId);
        if (existingRole == null)
        {
            return new BaseResponseModel
            {
                StatusCode = HttpStatusCode.NotFound,
                Data = false,
                Description = "Role not found"
            };
        }

        // Check if another role with the same name exists (excluding current role)
        var duplicateRole = await _UnitOfWorkApp.RoleDal.GetAsync(r =>
            r.RoleName.ToLower() == updateRoleDto.RoleName.ToLower() && r.Id != updateRoleDto.RoleId);

        if (duplicateRole != null)
        {
            return new BaseResponseModel
            {
                StatusCode = HttpStatusCode.Conflict,
                Data = false,
                Description = $"A role with name '{updateRoleDto.RoleName}' already exists"
            };
        }

        _IMapper.Map(updateRoleDto, existingRole);
        await _UnitOfWorkApp.RoleDal.UpdateAsync(existingRole);
        await _UnitOfWorkApp.SaveAsync();

        return new BaseResponseModel
        {
            StatusCode = HttpStatusCode.OK,
            Data = true,
            Description = "Role updated successfully"
        };
    }

    public async Task<BaseResponseModel> DeleteRoleAsync(int roleId)
    {
        var role = await _UnitOfWorkApp.RoleDal.GetByIdAsync(roleId);
        if (role == null)
        {
            return new BaseResponseModel
            {
                StatusCode = HttpStatusCode.NotFound,
                Description = "Role not found"
            };
        }

        // Check if role is being used by any users
        var usersWithRole = await _UnitOfWorkApp.UserRoleDal.GetAllAsync(ur => ur.RoleId == roleId);
        if (usersWithRole?.Count > 0)
        {
            return new BaseResponseModel
            {
                StatusCode = HttpStatusCode.Conflict,
                Data = false,
                Description = "Cannot delete role because it is assigned to users"
            };
        }

        await _UnitOfWorkApp.RoleDal.DeleteAsync(role);
        await _UnitOfWorkApp.SaveAsync();

        return new BaseResponseModel
        {
            StatusCode = HttpStatusCode.OK,
            Description = "Role deleted successfully"
        };
    }

    public async Task<BaseResponseModel> GetAllRolesAsync()
    {
        var roles = await _UnitOfWorkApp.RoleDal.GetAllAsync(r => r.IsActive);
        if (roles == null || roles.Count == 0)
        {
            return new BaseResponseModel
            {
                StatusCode = HttpStatusCode.NotFound,
                Description = "No roles found",
                Data = new List<RoleResponseDto>()
            };
        }

        var roleDtos = _IMapper.Map<List<RoleResponseDto>>(roles);

        return new BaseResponseModel
        {
            StatusCode = HttpStatusCode.OK,
            Description = "Roles retrieved successfully",
            Data = roleDtos
        };
    }

    public async Task<BaseResponseModel> GetRoleByIdAsync(int roleId)
    {
        var role = await _UnitOfWorkApp.RoleDal.GetByIdAsync(roleId);
        if (role == null)
        {
            return new BaseResponseModel
            {
                StatusCode = HttpStatusCode.NotFound,
                Description = "Role not found"
            };
        }

        var roleDto = _IMapper.Map<RoleResponseDto>(role);

        return new BaseResponseModel
        {
            StatusCode = HttpStatusCode.OK,
            Description = "Role retrieved successfully",
            Data = roleDto
        };
    }
}
