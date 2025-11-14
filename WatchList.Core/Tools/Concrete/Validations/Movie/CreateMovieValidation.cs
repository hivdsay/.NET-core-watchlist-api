using FluentValidation;
using WatchList.Core.Tools.Concrete.Dto.Movie.Request;

namespace WatchList.Core.Tools.Concrete.Validations.Movie;

public class CreateMovieValidation : AbstractValidator<CreateMovieDto>
{
    public CreateMovieValidation()
    {
        RuleFor(m => m.Title).NotEmpty().WithMessage("Title is required");
        RuleFor(m => m.Year).NotEmpty().WithMessage("Year is required");
        RuleFor(m => m.IMDbRating)
            .InclusiveBetween(0, 10)
            .WithMessage("IMDb rating must be between 0 and 10");
        
    }
    
}