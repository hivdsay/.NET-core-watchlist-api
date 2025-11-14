using System.Net;
using WatchList.Business.Abstract.App;
using WatchList.Business.Concrete.Generic;
using WatchList.Core.Tools.Concrete.Dto.Movie.Request;
using WatchList.DataAccess.Abstract.UnitOfWorkApp;
using WatchList.Core.Tools.Concrete.Validations.Movie;
using WatchList.Entities.Concrete;
using Newtonsoft.Json;
using AutoMapper;
using WatchList.Core.Tools.Concrete.Dto.Movie.Response;
using WatchList.DataAccess.Concrete.Repository;
using WatchList.Core.Tools.Concrete.Results;


namespace WatchList.Business.Concrete.App;

public class MovieManager : ManagerBase, IMovieService
{
    public MovieManager(IUnitOfWorkApp unitOfWorkApp, IMapper IMapper) : base(unitOfWorkApp, IMapper)
    {
    }

    public async Task<BaseResponseModel> CreateMovieAsync(CreateMovieDto createMovieDto)
    {
        var existingMovie = await _UnitOfWorkApp.MovieDal.GetAsync(
            m => m.Title.ToUpper() == createMovieDto.Title.ToUpper() 
                 && m.Year == createMovieDto.Year );

        if (existingMovie != null)
        {
            return new BaseResponseModel
            {
                StatusCode = HttpStatusCode.Conflict,
                Data = false,
                Description = $"A movie with the title '{createMovieDto.Title}' from year {createMovieDto.Year} already exists"
            };
        }
        
        // 3. Mapping + Kaydetme
        var mapData = _IMapper.Map<Movie>(createMovieDto);
        await _UnitOfWorkApp.MovieDal.AddAsync(mapData);
        await _UnitOfWorkApp.SaveAsync();

        return new BaseResponseModel
        {
            StatusCode = HttpStatusCode.Created,
            Data = true
        };
    }
    

    public async Task<BaseResponseModel> UpdateMovieAsync(UpdateMovieDto updateMovieDto)
    {

        // Check if the movie exists
        var existingMovie = await _UnitOfWorkApp.MovieDal.GetByIdAsync(updateMovieDto.Id);
        if (existingMovie == null)
        {
            return new BaseResponseModel
            {
                StatusCode = HttpStatusCode.BadRequest,
                Description = "Movie not found",
                Data = false
            };
        }
        
        // Map + Update
        _IMapper.Map(updateMovieDto, existingMovie); // DTO'dan entity'ye map et
        await _UnitOfWorkApp.MovieDal.UpdateAsync(existingMovie);
        await _UnitOfWorkApp.SaveAsync();

        return new BaseResponseModel
        {
            StatusCode = HttpStatusCode.OK,
            Description = "Movie updated successfully"
        };
    }


    public async Task<BaseResponseModel> DeleteMovieAsync(int movieId)
    {
        var movie = await _UnitOfWorkApp.MovieDal.GetByIdAsync(movieId);
        if (movie == null)
        {
            return new BaseResponseModel
            {
                StatusCode = HttpStatusCode.NotFound,
                Description = "Movie not found",
            };
        }
        await _UnitOfWorkApp.MovieDal.DeleteAsync(movie);
        await _UnitOfWorkApp.SaveAsync();
        return new BaseResponseModel
        {
            StatusCode = HttpStatusCode.OK,
            Description = "Movie deleted successfully",
            
        };
    }

    public async Task<BaseResponseModel> GetAllMoviesAsync()
    {
        var movies = await _UnitOfWorkApp.MovieDal.GetAllAsync(m => m.IsActive);
        if (movies == null || movies.Count == 0)
        {
            // Eğer liste null ise veya içinde hiç film yoksa
            return new BaseResponseModel
            {
                StatusCode = HttpStatusCode.NotFound,
                Description = "No movies found",
                Data = new List<MovieRepository>()
            };
        }
        var movieDtos = _IMapper.Map<List<MovieListResponseDto>>(movies);

        return new BaseResponseModel
        {
            StatusCode = HttpStatusCode.OK,
            Description = "Movies retrieved successfully",
            Data = movieDtos
        };
    }

    public async Task<BaseResponseModel> GetMovieByIdAsync(int movieId)
    {
        var movie = await _UnitOfWorkApp.MovieDal.GetByIdAsync(movieId);
        if (movie == null)
        {
            return new BaseResponseModel
            {
                StatusCode = HttpStatusCode.NotFound,
                Description = "Movie not found",
                Data = null
            };
        }
        var movieDto = _IMapper.Map<MovieResponseDto>(movie);
        return new BaseResponseModel
        {
            StatusCode = HttpStatusCode.OK,
            Data = movieDto,
            Description = "Movie retrieved successfully"
        };
    }
}