using FluentValidation;
using WatchList.Core.Tools.Concrete.Dto.Review.Request;

namespace WatchList.Core.Tools.Concrete.Validations.Review;

public class CreateReviewValidation : AbstractValidator<CreateReviewDto>
{
    public CreateReviewValidation()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage("UserId must be a valid positive integer.");


        RuleFor(x => x.MovieId)
            .GreaterThan(0)
            .WithMessage("MovieId must be a valid positive integer.");

    
        RuleFor(x => x.UserRating)
            .InclusiveBetween(1, 10)
            .WithMessage("UserRating must be between 1 and 10.");

        RuleFor(x => x.Comment)
            .NotEmpty()
            .WithMessage("Comment cannot be empty.")
            .MaximumLength(1000)
            .WithMessage("Comment cannot exceed 1000 characters.");
        
        RuleFor(x => x.ReviewTitle)
            .MaximumLength(100)
            .WithMessage("ReviewTitle cannot exceed 100 characters.");


    }

}    