using FluentValidation;
using WatchList.Core.Tools.Concrete.Dto.User.Request;

namespace WatchList.Core.Tools.Concrete.Validations.User;

public class LoginValidation : AbstractValidator<LoginRequestDto>
{
    public LoginValidation()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email alanı zorunludur")
            .EmailAddress().WithMessage("Geçerli bir email adresi giriniz")
            .MaximumLength(100).WithMessage("Email adresi 100 karakterden uzun olamaz");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Şifre alanı zorunludur")
            .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır")
            .MaximumLength(50).WithMessage("Şifre 50 karakterden uzun olamaz");
    }
}