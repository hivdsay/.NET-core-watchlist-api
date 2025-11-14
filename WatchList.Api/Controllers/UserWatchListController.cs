using Microsoft.AspNetCore.Mvc;
using System.Net;
using WatchList.Business.Abstract.App;
using WatchList.Business.Abstract.Generic;
using WatchList.Core.Tools.Concrete.Dto.UserWatchList.Request;
using WatchList.Core.Tools.Concrete.Results;
using WatchList.Core.Tools.Concrete.Validations.UserWatchList;

namespace WatchList.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserWatchListController(IGenericServiceApp genericServiceApp) : BaseWrapperController
{
    [HttpPost]
    public async Task<IActionResult> CreateUserWatchListAsync([FromBody] CreateUserWatchListDto dto)
    {
        var validation = new CreateUserWatchListValidation().Validate(dto);
        if (!validation.IsValid)
        {
            string errorMessages = string.Join(", ", validation.Errors.Select(x => x.ErrorMessage));
            return BadRequestResponse(errorMessages);
        }

        var response = await genericServiceApp.UserWatchListService.CreateUserWatchListAsync(dto);
        return BaseApiResponse(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUserWatchListAsync([FromBody] UpdateUserWatchListDto dto)
    {
        var validation = new UpdateUserWatchListValidation().Validate(dto);
        if (!validation.IsValid)
        {
            string errorMessages = string.Join(", ", validation.Errors.Select(x => x.ErrorMessage));
            return BadRequestResponse(errorMessages);
        }

        var response = await genericServiceApp.UserWatchListService.UpdateUserWatchListAsync(dto);
        return BaseApiResponse(response);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUserWatchListAsync([FromBody] DeleteUserWatchListRequestDto request)
    {
        if (request.Id <= 0)
        {
            return BaseApiResponse(new BaseResponseModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Description = "UserWatchList not found"
            });
        }

        var response = await genericServiceApp.UserWatchListService.DeleteUserWatchListAsync(request.Id);
        return BaseApiResponse(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUserWatchListsAsync()
    {
        var response = await genericServiceApp.UserWatchListService.GetAllUserWatchListsAsync();
        return BaseApiResponse(response);
    }

    [HttpPost("get-by-id")]
    public async Task<IActionResult> GetUserWatchListByIdAsync([FromBody] GetUserWatchListByIdRequestDto request)
    {
        if (request.Id <= 0)
            return BadRequestResponse("Invalid userWatchList id");

        var response = await genericServiceApp.UserWatchListService.GetUserWatchListByIdAsync(request.Id);
        return BaseApiResponse(response);
    }
    
}