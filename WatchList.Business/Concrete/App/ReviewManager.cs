using System.Net;
using AutoMapper;
using WatchList.Business.Abstract.App;
using WatchList.Business.Concrete.Generic;
using WatchList.Core.Tools.Concrete.Dto.Review.Request;
using WatchList.Core.Tools.Concrete.Dto.Review.Response;
using WatchList.DataAccess.Abstract.UnitOfWorkApp;
using WatchList.Core.Tools.Concrete.Results;
using WatchList.Entities.Concrete;

namespace WatchList.Business.Concrete.App;

public class ReviewManager : ManagerBase, IReviewService
{
    public ReviewManager(IUnitOfWorkApp unitOfWorkApp, IMapper IMapper) : base(unitOfWorkApp, IMapper)
    {
    }

    public async Task<BaseResponseModel> CreateReviewAsync(CreateReviewDto createReviewDto)
    {
        // User kontrolü
        var userCheck = await _UnitOfWorkApp.UserDal.GetByIdAsync(createReviewDto.UserId);
        if (userCheck == null)
            return new BaseResponseModel 
            { 
                StatusCode = HttpStatusCode.BadRequest, 
                Description = "User not found." 
            };

        // Movie kontrolü
        var movieChek = await _UnitOfWorkApp.MovieDal.GetByIdAsync(createReviewDto.MovieId);
        if (movieChek == null)
            return new BaseResponseModel 
            { 
                StatusCode = HttpStatusCode.BadRequest, 
                Description = "Movie not found." 
            };

        // Duplicate kontrolü
        var existingReview = await _UnitOfWorkApp.ReviewDal
            .GetAsync(r => r.UserId == createReviewDto.UserId && r.MovieId == createReviewDto.MovieId);
        if (existingReview != null)
            return new BaseResponseModel 
            { 
                StatusCode = HttpStatusCode.Conflict, 
                Description = "This user has already reviewed this movie." 
            };

       
        var mapData = _IMapper.Map<Review>(createReviewDto);
        await _UnitOfWorkApp.ReviewDal.AddAsync(mapData);
        await _UnitOfWorkApp.SaveAsync();

        return new BaseResponseModel
        {
            StatusCode = HttpStatusCode.Created,
            Description = "Review created successfully."
        };
    }

    public async Task<BaseResponseModel> UpdateReviewAsync(UpdateReviewDto updateReviewDto)
    {
        var existingReview = await _UnitOfWorkApp.ReviewDal.GetByIdAsync(updateReviewDto.Id);
        if (existingReview == null)
        {
            return new BaseResponseModel
            {
                StatusCode = HttpStatusCode.NotFound,
                Data = false,
                Description = "Review not found."
            };
        }

        _IMapper.Map(updateReviewDto, existingReview);
        await _UnitOfWorkApp.ReviewDal.UpdateAsync(existingReview);
        await _UnitOfWorkApp.SaveAsync();

        return new BaseResponseModel
        {
            StatusCode = HttpStatusCode.OK,
            Data = true,
            Description = "Review updated successfully."
        };
    }

    public async Task<BaseResponseModel> DeleteReviewAsync(int reviewId)
    {
        var review = await _UnitOfWorkApp.ReviewDal.GetByIdAsync(reviewId);
        if (review == null)
        {
            return new BaseResponseModel
            {
                StatusCode = HttpStatusCode.NotFound,
                Description = "Review not found."
            };
        }

        await _UnitOfWorkApp.ReviewDal.DeleteAsync(review);
        await _UnitOfWorkApp.SaveAsync();

        return new BaseResponseModel
        {
            StatusCode = HttpStatusCode.OK,
            Description = "Review deleted successfully."
        };
    }

    public async Task<BaseResponseModel> GetAllReviewsAsync()
    {
        var reviews = await _UnitOfWorkApp.ReviewDal.GetAllAsync(r => r.IsActive);
        if (reviews == null || reviews.Count == 0)
        {
            return new BaseResponseModel
            {
                StatusCode = HttpStatusCode.NotFound,
                Description = "No reviews found.",
                Data = new List<ResponseReviewListDto>()
            };
        }

        var reviewDtos = _IMapper.Map<List<ResponseReviewListDto>>(reviews);

        return new BaseResponseModel
        {
            StatusCode = HttpStatusCode.OK,
            Description = "Reviews retrieved successfully.",
            Data = reviewDtos
        };
    }

    public async Task<BaseResponseModel> GetReviewByIdAsync(int reviewId)
    {
        var review = await _UnitOfWorkApp.ReviewDal.GetByIdAsync(reviewId);
        if (review == null)
        {
            return new BaseResponseModel
            {
                StatusCode = HttpStatusCode.NotFound,
                Description = "Review not found.",
                Data = null
            };
        }

        var reviewDto = _IMapper.Map<ResponseReviewDto>(review);

        return new BaseResponseModel
        {
            StatusCode = HttpStatusCode.OK,
            Description = "Review retrieved successfully.",
            Data = reviewDto
        };
    }
}