using Microsoft.AspNetCore.Mvc;
using System.Net;
using WatchList.Business.Abstract.Generic;
using WatchList.Core.Tools.Concrete.Dto.Role.Request;
using WatchList.Core.Tools.Concrete.Results;
using WatchList.Core.Tools.Concrete.Validations.Role;

namespace WatchList.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoleController(IGenericServiceApp genericServiceApp) : BaseWrapperController
{
    [HttpPost]
    public async Task<IActionResult> CreateRoleAsync([FromBody] CreateRoleDto dto)
    {
        var validation = new CreateRoleValidation().Validate(dto);
        if (!validation.IsValid)
        {
            string errorMessages = string.Join(", ", validation.Errors.Select(x => x.ErrorMessage));
            return BadRequestResponse(errorMessages);
        }

        var response = await genericServiceApp.RoleService.CreateRoleAsync(dto);
        return BaseApiResponse(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateRoleAsync([FromBody] UpdateRoleDto dto)
    {
        var validation = new UpdateRoleValidation().Validate(dto);
        if (!validation.IsValid)
        {
            string errorMessages = string.Join(", ", validation.Errors.Select(x => x.ErrorMessage));
            return BadRequestResponse(errorMessages);
        }

        var response = await genericServiceApp.RoleService.UpdateRoleAsync(dto);
        return BaseApiResponse(response);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteRoleAsync([FromBody] DeleteRoleRequestDto request)
    {
        if (request.Id <= 0)
        {
            return BaseApiResponse(new BaseResponseModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Description = "Role not found"
            });
        }

        var response = await genericServiceApp.RoleService.DeleteRoleAsync(request.Id);
        return BaseApiResponse(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRolesAsync()
    {
        var response = await genericServiceApp.RoleService.GetAllRolesAsync();
        return BaseApiResponse(response);
    }

    [HttpPost("get-by-id")]
    public async Task<IActionResult> GetRoleByIdAsync([FromBody] GetRoleByIdRequestDto request)
    {
        if (request.Id <= 0)
            return BadRequestResponse("Invalid role id");

        var response = await genericServiceApp.RoleService.GetRoleByIdAsync(request.Id);
        return BaseApiResponse(response);
    }
}