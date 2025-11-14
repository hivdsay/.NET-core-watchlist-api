using FluentValidation;
using WatchList.Core.Tools.Concrete.Dto.Movie.Request;

namespace WatchList.Core.Tools.Concrete.Validations.Movie;

public class UpdateMovieValidation : AbstractValidator<UpdateMovieDto>
{
    public UpdateMovieValidation()
    {
        RuleFor(m => m.Id)
            .NotEmpty()
            .WithMessage("Movie Id is required");

        // Title boş olamaz
        RuleFor(m => m.Title)
            .NotEmpty()
            .WithMessage("Title is required");

        // Year boş olamaz
        RuleFor(m => m.Year)
            .NotEmpty()
            .WithMessage("Year is required");

        // IMDb rating 0-10 arası olmalı
        RuleFor(m => m.IMDbRating)
            .InclusiveBetween(0, 10)
            .WithMessage("IMDb rating must be between 0 and 10");
    }
    
}