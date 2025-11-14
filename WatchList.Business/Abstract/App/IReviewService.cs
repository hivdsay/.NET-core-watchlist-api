using WatchList.Core.Tools.Concrete.Dto.Review.Request;
using WatchList.Core.Tools.Concrete.Results;

using WatchList.Entities.Concrete;

namespace WatchList.Business.Abstract.App;

public interface IReviewService
{
    Task<BaseResponseModel> CreateReviewAsync(CreateReviewDto createReviewDto);
    Task<BaseResponseModel> UpdateReviewAsync(UpdateReviewDto updateReviewDto);
    Task<BaseResponseModel> DeleteReviewAsync(int reviewId);
    Task<BaseResponseModel> GetAllReviewsAsync();
    Task<BaseResponseModel> GetReviewByIdAsync(int reviewId);
}