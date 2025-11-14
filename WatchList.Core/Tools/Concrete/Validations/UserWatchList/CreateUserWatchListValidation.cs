using FluentValidation;
using WatchList.Core.Tools.Concrete.Dto.UserWatchList.Request;

namespace WatchList.Core.Tools.Concrete.Validations.UserWatchList;

public class CreateUserWatchListValidation : AbstractValidator<CreateUserWatchListDto>
{
    public CreateUserWatchListValidation()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required")
            .GreaterThan(0).WithMessage("User ID must be greater than 0");

        RuleFor(x => x.MovieId)
            .NotEmpty().WithMessage("Movie ID is required")
            .GreaterThan(0).WithMessage("Movie ID must be greater than 0");

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status is required");

        RuleFor(x => x.PersonalRating)
            .InclusiveBetween(1, 10).WithMessage("Personal rating must be between 1 and 10");

        RuleFor(x => x.Notes)
            .MaximumLength(500).WithMessage("Notes cannot exceed 500 characters");
    }
    
}