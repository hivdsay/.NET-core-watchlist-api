using Microsoft.AspNetCore.Mvc;
using System.Net;
using WatchList.Business.Abstract.Generic;
using WatchList.Core.Tools.Concrete.Dto.Review.Request;
using WatchList.Core.Tools.Concrete.Results;
using WatchList.Core.Tools.Concrete.Validations.Review;

namespace WatchList.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewController(IGenericServiceApp genericServiceApp) : BaseWrapperController
{
    [HttpPost]
    public async Task<IActionResult> CreateReviewAsync([FromBody] CreateReviewDto dto)
    {
        var validation = new CreateReviewValidation().Validate(dto);
        if (!validation.IsValid)
        {
            string errorMessages = string.Join(", ", validation.Errors.Select(x => x.ErrorMessage));
            return BadRequestResponse(errorMessages);
        }

        var response = await genericServiceApp.ReviewService.CreateReviewAsync(dto);
        return BaseApiResponse(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateReviewAsync([FromBody] UpdateReviewDto dto)
    {
        var validation = new UpdateReviewValidation().Validate(dto);
        if (!validation.IsValid)
        {
            string errorMessages = string.Join(", ", validation.Errors.Select(x => x.ErrorMessage));
            return BadRequestResponse(errorMessages);
        }

        var response = await genericServiceApp.ReviewService.UpdateReviewAsync(dto);
        return BaseApiResponse(response);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteReviewAsync([FromBody] DeleteReviewRequestDto request)
    {
        if (request.Id <= 0)
        {
            return BaseApiResponse(new BaseResponseModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Description = "Review not found"
            });
        }

        var response = await genericServiceApp.ReviewService.DeleteReviewAsync(request.Id);
        return BaseApiResponse(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllReviewsAsync()
    {
        var response = await genericServiceApp.ReviewService.GetAllReviewsAsync();
        return BaseApiResponse(response);
    }

    [HttpPost("get-by-id")]
    public async Task<IActionResult> GetReviewByIdAsync([FromBody] GetReviewByIdRequestDto request)
    {
        if (request.Id <= 0)
            return BadRequestResponse("Invalid review id");

        var response = await genericServiceApp.ReviewService.GetReviewByIdAsync(request.Id);
        return BaseApiResponse(response);
    }
}
