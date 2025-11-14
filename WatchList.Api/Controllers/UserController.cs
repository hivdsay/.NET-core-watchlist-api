using Microsoft.AspNetCore.Mvc;
using System.Net;
using WatchList.Business.Abstract.Generic;
using WatchList.Core.Tools.Concrete.Dto.User.Request;
using WatchList.Core.Tools.Concrete.Results;
using WatchList.Core.Tools.Concrete.Validations.User;

namespace WatchList.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IGenericServiceApp genericServiceApp) : BaseWrapperController
{
    [HttpPost]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserDto dto)
    {
        var validation = new CreateUserValidation().Validate(dto);
        if (!validation.IsValid)
        {
            string errorMessages = string.Join(", ", validation.Errors.Select(x => x.ErrorMessage));
            return BadRequestResponse(errorMessages);
        }

        var response = await genericServiceApp.UserService.CreateUserAsync(dto);
        return BaseApiResponse(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateUserDto dto)
    {
        var validation = new UpdateUserValidation().Validate(dto);
        if (!validation.IsValid)
        {
            string errorMessages = string.Join(", ", validation.Errors.Select(x => x.ErrorMessage));
            return BadRequestResponse(errorMessages);
        }

        var response = await genericServiceApp.UserService.UpdateUserAsync(dto);
        return BaseApiResponse(response);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUserAsync([FromBody] DeleteUserRequestDto request)
    {
        if (request.Id <= 0)
        {
            return BaseApiResponse(new BaseResponseModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Description = "User not found"
            });
        }

        var response = await genericServiceApp.UserService.DeleteUserAsync(request.Id);
        return BaseApiResponse(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsersAsync()
    {
        var response = await genericServiceApp.UserService.GetAllUsersAsync();
        return BaseApiResponse(response);
    }

    [HttpPost("get-by-id")]
    public async Task<IActionResult> GetUserByIdAsync([FromBody] GetUserByIdRequestDto request)
    {
        if (request.Id <= 0)
            return BadRequestResponse("Invalid user id");

        var response = await genericServiceApp.UserService.GetUserByIdAsync(request.Id);
        return BaseApiResponse(response);
    }
    
    [HttpGet("get-user-list")]
    public async Task<IActionResult> GetUserList()
    {
        var response = await genericServiceApp.UserService.GetUserRoleAsync();
        return BaseApiResponse(response);
    }
}