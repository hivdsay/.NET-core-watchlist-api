using FluentValidation;
using WatchList.Core.Tools.Concrete.Dto.UserWatchList.Request;

namespace WatchList.Core.Tools.Concrete.Validations.UserWatchList;

public class UpdateUserWatchListValidation : AbstractValidator<UpdateUserWatchListDto>
{
    public UpdateUserWatchListValidation()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("WatchList ID is required")
            .GreaterThan(0).WithMessage("WatchList ID must be greater than 0");

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status is required");

        RuleFor(x => x.PersonalRating)
            .InclusiveBetween(1, 10).WithMessage("Personal rating must be between 1 and 10");

        RuleFor(x => x.Notes)
            .MaximumLength(500).WithMessage("Notes cannot exceed 500 characters");

        // WatchedDate sadece Status "Watched" ise gerekli
        RuleFor(x => x.WatchedDate)
            .NotEmpty().WithMessage("Watched date is required when status is 'Watched'")
            .When(x => x.Status == "Watched");

        // WatchedDate gelecek tarih olamaz
        RuleFor(x => x.WatchedDate)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Watched date cannot be in the future")
            .When(x => x.WatchedDate.HasValue);
    }

}