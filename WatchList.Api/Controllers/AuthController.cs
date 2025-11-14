using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WatchList.Business.Abstract.Generic;
using WatchList.Core.Tools.Abstract.JwtTool;
using WatchList.Core.Tools.Concrete.Dto.User.Request;
using WatchList.Core.Tools.Concrete.Dto.User.Response;
using WatchList.Core.Tools.Concrete.Results;
using WatchList.Core.Tools.Concrete.Validations.User;

namespace WatchList.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IGenericServiceApp genericServiceApp, IJwtService jwtService, IMapper mapper)
    : BaseWrapperController
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        var validation = new LoginValidation().Validate(request);
        if (!validation.IsValid)
        {
            string errorMessages = string.Join(", ", validation.Errors.Select(x => x.ErrorMessage));
            return BadRequestResponse(errorMessages);
        }

        var checkUser = await genericServiceApp.UserService.UserLoginCheck(request);
        if (checkUser != null)
        {
            LoginResponseDto response = new();
            response.User = mapper.Map<UserInfoDto>(checkUser);
           
            response.Token = await jwtService.GenerateJwt(checkUser, checkUser.UserRoles);
            

            return BaseApiResponse(new BaseResponseModel
            {
                StatusCode = HttpStatusCode.OK,
                Data = response
            });

        }
        return BaseApiResponse(new BaseResponseModel
        {
            StatusCode = HttpStatusCode.BadRequest,
            Description = "Kullanıcı adı yada şifre hatalı"
        });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateUserDto request)
    {
        var validation = new CreateUserValidation().Validate(request);
        if (!validation.IsValid)
        {
            string errorMessages = string.Join(", ", validation.Errors.Select(x => x.ErrorMessage));
            return BadRequestResponse(errorMessages);
        }

        var result = await genericServiceApp.UserService.CreateUserAsync(request);
        return BaseApiResponse(result);
    }
}       
