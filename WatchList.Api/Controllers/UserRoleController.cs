using Microsoft.AspNetCore.Mvc;
using System.Net;
using WatchList.Business.Abstract.Generic;
using WatchList.Core.Tools.Concrete.Dto.UserRole.Request;
using WatchList.Core.Tools.Concrete.Results;
using WatchList.Core.Tools.Concrete.Validations.UserRole;

namespace WatchList.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserRoleController(IGenericServiceApp genericServiceApp) : BaseWrapperController
{
    [HttpPost]
    public async Task<IActionResult> CreateUserRoleAsync([FromBody] CreateUserRoleDto dto)
    {
        var validation = new CreateUserRoleValidation().Validate(dto);
        if (!validation.IsValid)
        {
            string errorMessages = string.Join(", ", validation.Errors.Select(x => x.ErrorMessage));
            return BadRequestResponse(errorMessages);
        }

        var response = await genericServiceApp.UserRoleService.CreateUserRoleAsync(dto);
        return BaseApiResponse(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUserRoleAsync([FromBody] UpdateUserRoleDto dto)
    {
        var validation = new UpdateUserRoleValidation().Validate(dto);
        if (!validation.IsValid)
        {
            string errorMessages = string.Join(", ", validation.Errors.Select(x => x.ErrorMessage));
            return BadRequestResponse(errorMessages);
        }

        var response = await genericServiceApp.UserRoleService.UpdateUserRoleAsync(dto);
        return BaseApiResponse(response);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUserRoleAsync([FromBody] DeleteUserRoleRequestDto request)
    {
        if (request.Id <= 0)
        {
            return BaseApiResponse(new BaseResponseModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Description = "User role relationship not found"
            });
        }

        var response = await genericServiceApp.UserRoleService.DeleteUserRoleAsync(request.Id);
        return BaseApiResponse(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUserRolesAsync()
    {
        var response = await genericServiceApp.UserRoleService.GetAllUserRolesAsync();
        return BaseApiResponse(response);
    }

    [HttpPost("get-by-id")]
    public async Task<IActionResult> GetUserRoleByIdAsync([FromBody] GetUserRoleByIdRequestDto request)
    {
        if (request.Id <= 0)
            return BadRequestResponse("Invalid user role id");

        var response = await genericServiceApp.UserRoleService.GetUserRoleByIdAsync(request.Id);
        return BaseApiResponse(response);
    }
}