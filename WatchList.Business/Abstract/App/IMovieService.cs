using WatchList.Core.Tools.Concrete.Dto.Movie.Request;
using WatchList.Core.Tools.Concrete.Dto.Movie.Response;
using WatchList.Core.Tools.Concrete.Results;
using WatchList.Entities.Concrete;

namespace WatchList.Business.Abstract.App;

public interface IMovieService
{
 
    Task<BaseResponseModel> CreateMovieAsync(CreateMovieDto createMovieDto);
    Task<BaseResponseModel> UpdateMovieAsync(UpdateMovieDto updateMovieDto);
    Task<BaseResponseModel> DeleteMovieAsync(int movieId);
    Task<BaseResponseModel> GetAllMoviesAsync(); 
    // Dönen Data: List<MovieListResponseDto>
    Task<BaseResponseModel> GetMovieByIdAsync(int movieId); 
    // Dönen Data: MovieResponseDto
    
}