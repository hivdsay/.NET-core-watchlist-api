using System.Net;
using AutoMapper;
using WatchList.Business.Abstract.App;
using WatchList.Business.Concrete.Generic;
using WatchList.Core.Tools.Concrete.Results;
using WatchList.Core.Tools.Concrete.Dto.User.Request;
using WatchList.Core.Tools.Concrete.Dto.User.Response;
using WatchList.Core.Tools.Concrete.PasswordHashing;
using WatchList.DataAccess.Abstract.UnitOfWorkApp;
using WatchList.DataAccess.Concrete.Repository;
using WatchList.Entities.Concrete;

namespace WatchList.Business.Concrete.App;

public class UserManager : ManagerBase, IUserService
{
    public UserManager(IUnitOfWorkApp unitOfWorkApp, IMapper IMapper) : base(unitOfWorkApp, IMapper)
    {
    }

    public async Task<BaseResponseModel> CreateUserAsync(CreateUserDto createUserDto)
    {
        var existingUser = await _UnitOfWorkApp.UserDal.GetAsync(u =>
            u.Email.ToLower() == createUserDto.Email.ToLower());

        if (existingUser != null)
        {
            return new BaseResponseModel
            {
                StatusCode = HttpStatusCode.Conflict,
                Data = false,
                Description = $"A user with email '{createUserDto.Email}' already exists"
            };
        }

        var user = _IMapper.Map<User>(createUserDto);
        user.Password = PasswordHash.HashHMACHex(createUserDto.Password);
        var savedUser =  await _UnitOfWorkApp.UserDal.AddAsync(user);
        await _UnitOfWorkApp.SaveAsync();
        
        List<UserRole> userRoles = new();
        createUserDto.UserRolesx.ForEach(x =>
        {
            userRoles.Add(new UserRole()
            {
                UserId = savedUser.Id,
                RoleId = x
            });
        });
        
         await _UnitOfWorkApp.UserRoleDal.AddBulkAsync(userRoles);
         await _UnitOfWorkApp.SaveAsync();

        return new BaseResponseModel
        {
            StatusCode = HttpStatusCode.Created,
            Data = true,
            Description = "User created successfully"
        };
    }

    public async Task<BaseResponseModel> UpdateUserAsync(UpdateUserDto updateUserDto)
    {
        var existingUser = await _UnitOfWorkApp.UserDal.GetByIdAsync(updateUserDto.Id);
        if (existingUser == null)
        {
            return new BaseResponseModel
            {
                StatusCode = HttpStatusCode.NotFound,
                Data = false,
                Description = "User not found"
            };
        }

        _IMapper.Map(updateUserDto, existingUser);
        if (!string.IsNullOrEmpty(updateUserDto.Password))
        {
            existingUser.Password = PasswordHash.HashHMACHex(updateUserDto.Password);
        }
        await _UnitOfWorkApp.UserDal.UpdateAsync(existingUser);
        await _UnitOfWorkApp.SaveAsync();

        return new BaseResponseModel
        {
            StatusCode = HttpStatusCode.OK,
            Data = true,
            Description = "User updated successfully"
        };
    }

    public async Task<BaseResponseModel> DeleteUserAsync(int userId)
    {
        var user = await _UnitOfWorkApp.UserDal.GetByIdAsync(userId);
        if (user == null)
        {
            return new BaseResponseModel
            {
                StatusCode = HttpStatusCode.NotFound,
                Description = "User not found"
            };
        }

        await _UnitOfWorkApp.UserDal.DeleteAsync(user);
        await _UnitOfWorkApp.SaveAsync();

        return new BaseResponseModel
        {
            StatusCode = HttpStatusCode.OK,
            Description = "User deleted successfully"
        };
    }

    public async Task<BaseResponseModel> GetAllUsersAsync()
    {
        var users = await _UnitOfWorkApp.UserDal.GetAllAsync(u => u.IsActive);
        if (users == null || users.Count == 0)
        {
            return new BaseResponseModel
            {
                StatusCode = HttpStatusCode.NotFound,
                Description = "No users found",
                Data = new List<UserResponseDto>()
            };
        }

        var userDtos = _IMapper.Map<List<UserResponseDto>>(users);

        return new BaseResponseModel
        {
            StatusCode = HttpStatusCode.OK,
            Description = "Users retrieved successfully",
            Data = userDtos
        };
    }

    public async Task<BaseResponseModel> GetUserByIdAsync(int userId)
    {
        var user = await _UnitOfWorkApp.UserDal.GetByIdAsync(userId);
        if (user == null)
        {
            return new BaseResponseModel
            {
                StatusCode = HttpStatusCode.NotFound,
                Description = "User not found"
            };
        }

        var userDto = _IMapper.Map<UserResponseDto>(user);

        return new BaseResponseModel
        {
            StatusCode = HttpStatusCode.OK,
            Description = "User retrieved successfully",
            Data = userDto
        };
    }

    public async Task<BaseResponseModel> GetUserRoleAsync()
    {
        var userList = await _UnitOfWorkApp.UserDal.GetUserRoleAsync();
        if (userList?.Count > 0)
        {
            return new BaseResponseModel
            {
                StatusCode = HttpStatusCode.OK,
                Data = userList
            };
        }
        return new BaseResponseModel
        {
            StatusCode = HttpStatusCode.NoContent,
            Description = "User not found",
            Data = new List<UserRoleRepository>(),
        };
    }
    public async Task<User> UserLoginCheck(LoginRequestDto request)
    {

        string hashedPassword = PasswordHash.HashHMACHex(request.Password);
    
        var response = await _UnitOfWorkApp.UserDal.GetAsync(x => x.Email == request.Email 
                                                                  && x.Password == hashedPassword,
            s => s.UserRoles);
        return response;
    } 
}
