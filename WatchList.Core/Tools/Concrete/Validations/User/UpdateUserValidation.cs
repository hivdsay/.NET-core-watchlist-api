using FluentValidation;
using WatchList.Core.Tools.Concrete.Dto.User.Request;

namespace WatchList.Core.Tools.Concrete.Validations.User;

public class UpdateUserValidation : AbstractValidator<UpdateUserDto>
{
    public UpdateUserValidation()
    {
        RuleFor(u => u.Id)
            .NotEmpty().WithMessage("User ID is required")
            .GreaterThan(0).WithMessage("User ID must be greater than 0");

        RuleFor(u => u.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .Length(2, 50).WithMessage("First name must be between 2 and 50 characters");
        
        RuleFor(u => u.LastName)
            .NotEmpty().WithMessage("Last name is required")
            .Length(2, 50).WithMessage("Last name must be between 2 and 50 characters");

        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Please enter a valid email address")
            .MaximumLength(100).WithMessage("Email cannot exceed 100 characters");

        // Password update için opsiyonel - boş bırakılabilir ama verilirse validasyon yapılır
        RuleFor(u => u.Password)
            .MinimumLength(6).WithMessage("Password must be at least 6 characters")
            .MaximumLength(100).WithMessage("Password cannot exceed 100 characters");
    }
    
}