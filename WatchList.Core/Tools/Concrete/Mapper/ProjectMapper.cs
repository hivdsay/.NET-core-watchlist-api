using AutoMapper;
using WatchList.Core.Tools.Concrete.Dto.Movie.Request;
using WatchList.Core.Tools.Concrete.Dto.Movie.Response;
using WatchList.Core.Tools.Concrete.Dto.Review.Request;
using WatchList.Core.Tools.Concrete.Dto.Review.Response;
using WatchList.Core.Tools.Concrete.Dto.User.Request;
using WatchList.Core.Tools.Concrete.Dto.User.Response;
using WatchList.Core.Tools.Concrete.Dto.UserWatchList.Request;
using WatchList.Core.Tools.Concrete.Dto.UserWatchList.Response;
using WatchList.Entities.Concrete;

namespace WatchList.Core.Tools.Concrete.Mapper;

public class ProjectMapper : Profile
{
    public ProjectMapper()
    {
        Movie();
        Review();
        User();
        UserWatchList();
    }
    void Movie()
    {
        CreateMap<CreateMovieDto, Movie>().ReverseMap();
        CreateMap<UpdateMovieDto, Movie>().ReverseMap();
        CreateMap<Movie, MovieResponseDto>().ReverseMap();
        CreateMap<Movie, MovieListResponseDto>().ReverseMap();
    }
    void Review()
    {
        CreateMap<CreateReviewDto, Review>().ReverseMap();
        CreateMap<UpdateReviewDto, Review>().ReverseMap();
        CreateMap<Review, ResponseReviewDto>().ReverseMap();
        CreateMap<Review, ResponseReviewListDto>().ReverseMap();
    }

    void User()
    {
        CreateMap<CreateUserDto, User>().ReverseMap();
        CreateMap<UpdateUserDto, User>().ReverseMap();
        CreateMap<User, UserResponseDto>().ReverseMap();
        CreateMap<LoginResponseDto,User>().ReverseMap();
        CreateMap<User, UserInfoDto>().ReverseMap();
        CreateMap<LoginRequestDto, User>().ReverseMap();
    }

    void UserWatchList()
    {
        CreateMap<CreateUserWatchListDto, UserWatchList>().ReverseMap();
        CreateMap<UpdateUserWatchListDto, UserWatchList>().ReverseMap();
        CreateMap<UserWatchList, UserWatchListResponseDto>().ReverseMap();
        CreateMap<UserWatchList, UserWatchListsResponseDto>().ReverseMap();
    }
    
}