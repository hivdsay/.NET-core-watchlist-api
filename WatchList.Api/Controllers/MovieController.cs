using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WatchList.Business.Abstract.App;
using WatchList.Business.Abstract.Generic;
using WatchList.Core.Tools.Concrete.Dto.Movie.Request;
using WatchList.Core.Tools.Concrete.Results;
using WatchList.Core.Tools.Concrete.Validations.Movie;

namespace WatchList.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MovieController(IGenericServiceApp genericServiceApp) : BaseWrapperController
{
    
    [HttpPost]
    public async Task<IActionResult> CreateMovieAsync([FromBody] CreateMovieDto dto)
    {
        var validation = new CreateMovieValidation().Validate(dto);
        if (!validation.IsValid)
        {
            string errorMessages = string.Join(", ", validation.Errors.Select(x => x.ErrorMessage));
            return BadRequestResponse(errorMessages);
        }
      
        var response = await genericServiceApp.MovieService.CreateMovieAsync(dto);
        return BaseApiResponse(response);
    }
    [HttpPut]
    public async Task<IActionResult> UpdateMovieAsync([FromBody] UpdateMovieDto dto)
    {
        var validation = new UpdateMovieValidation().Validate(dto);
        if (!validation.IsValid)
        {
            string errorMessages = string.Join(", ", validation.Errors.Select(x => x.ErrorMessage));
            return BadRequestResponse(errorMessages);
        }

        var response = await genericServiceApp.MovieService.UpdateMovieAsync(dto);
        return BaseApiResponse(response);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteMovieAsync([FromBody] DeleteMovieRequestDto request)
    {
        if (request.Id <= 0)
        {
            return BaseApiResponse(new BaseResponseModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Description = "Movie not found"
            });
        }

        var response = await genericServiceApp.MovieService.DeleteMovieAsync(request.Id);
        return BaseApiResponse(response);
    }


    [HttpGet]
    public async Task<IActionResult> GetAllMoviesAsync()
    {
        var response = await genericServiceApp.MovieService.GetAllMoviesAsync();
        return BaseApiResponse(response);
    }

    [HttpPost("get-by-id")]
    public async Task<IActionResult> GetMovieByIdAsync([FromBody] GetMovieByIdRequestDto request)
    {
        if (request.Id <= 0)
            return BadRequestResponse("Invalid movie id");

        var response = await genericServiceApp.MovieService.GetMovieByIdAsync(request.Id);
        return BaseApiResponse(response);
    }
}

