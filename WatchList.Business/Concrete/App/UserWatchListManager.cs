using System.Net;
using AutoMapper;
using WatchList.Business.Abstract.App;
using WatchList.Business.Concrete.Generic;
using WatchList.Core.Tools.Concrete.Dto.UserWatchList.Request;
using WatchList.Core.Tools.Concrete.Dto.UserWatchList.Response;
using WatchList.Core.Tools.Concrete.Results;
using WatchList.DataAccess.Abstract.UnitOfWorkApp;
using WatchList.Entities.Concrete;

namespace WatchList.Business.Concrete.App;

public class UserWatchListManager : ManagerBase, IUserWatchListService
{
    public UserWatchListManager(IUnitOfWorkApp unitOfWorkApp, IMapper mapper) : base(unitOfWorkApp, mapper)
    {
    }

    public async Task<BaseResponseModel> CreateUserWatchListAsync(CreateUserWatchListDto createUserWatchListDto)
    {
        var user = await _UnitOfWorkApp.UserDal.GetByIdAsync(createUserWatchListDto.UserId);
        if (user == null)
            return new BaseResponseModel { StatusCode = HttpStatusCode.BadRequest, Description = "User not found." };

        var movie = await _UnitOfWorkApp.MovieDal.GetByIdAsync(createUserWatchListDto.MovieId);
        if (movie == null)
            return new BaseResponseModel { StatusCode = HttpStatusCode.BadRequest, Description = "Movie not found." };

        var existing = await _UnitOfWorkApp.UserWatchListDal
            .GetAsync(x => x.UserId == createUserWatchListDto.UserId && x.MovieId == createUserWatchListDto.MovieId);
        if (existing != null)
            return new BaseResponseModel { StatusCode = HttpStatusCode.Conflict, Description = "This movie is already in the user's watchlist." };

        var mapData = _IMapper.Map<UserWatchList>(createUserWatchListDto);
        await _UnitOfWorkApp.UserWatchListDal.AddAsync(mapData);
        await _UnitOfWorkApp.SaveAsync();

        return new BaseResponseModel { StatusCode = HttpStatusCode.Created, Description = "Watchlist entry created successfully." };
    }

    public async Task<BaseResponseModel> UpdateUserWatchListAsync(UpdateUserWatchListDto updateUserWatchListDto)
    {
        var existing = await _UnitOfWorkApp.UserWatchListDal.GetByIdAsync(updateUserWatchListDto.UserWatchListId);
        if (existing == null)
            return new BaseResponseModel { StatusCode = HttpStatusCode.NotFound, Description = "Watchlist entry not found.", Data = false };

        _IMapper.Map(updateUserWatchListDto, existing);
        await _UnitOfWorkApp.UserWatchListDal.UpdateAsync(existing);
        await _UnitOfWorkApp.SaveAsync();

        return new BaseResponseModel { StatusCode = HttpStatusCode.OK, Description = "Watchlist entry updated successfully.", Data = true };
    }

    public async Task<BaseResponseModel> DeleteUserWatchListAsync(int id)
    {
        var existing = await _UnitOfWorkApp.UserWatchListDal.GetByIdAsync(id);
        if (existing == null)
            return new BaseResponseModel { StatusCode = HttpStatusCode.NotFound, Description = "Watchlist entry not found." };

        await _UnitOfWorkApp.UserWatchListDal.DeleteAsync(existing);
        await _UnitOfWorkApp.SaveAsync();

        return new BaseResponseModel { StatusCode = HttpStatusCode.OK, Description = "Watchlist entry deleted successfully." };
    }

    public async Task<BaseResponseModel> GetAllUserWatchListsAsync()
    {
        var entries = await _UnitOfWorkApp.UserWatchListDal.GetAllAsync(x => x.IsActive);
        if (entries == null || entries.Count == 0)
            return new BaseResponseModel { StatusCode = HttpStatusCode.NotFound, Description = "No watchlist entries found.", Data = new List<UserWatchListsResponseDto>() };

        var dtoList = _IMapper.Map<List<UserWatchListsResponseDto>>(entries);

        return new BaseResponseModel { StatusCode = HttpStatusCode.OK, Description = "Watchlist entries retrieved successfully.", Data = dtoList };
    }

    public async Task<BaseResponseModel> GetUserWatchListByIdAsync(int id)
    {
        var entry = await _UnitOfWorkApp.UserWatchListDal.GetByIdAsync(id);
        if (entry == null)
            return new BaseResponseModel { StatusCode = HttpStatusCode.NotFound, Description = "Watchlist entry not found.", Data = null };

        var dto = _IMapper.Map<UserWatchListResponseDto>(entry);

        return new BaseResponseModel { StatusCode = HttpStatusCode.OK, Description = "Watchlist entry retrieved successfully.", Data = dto };
    }
}
