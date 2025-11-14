using FluentValidation;
using WatchList.Core.Tools.Concrete.Dto.Role.Request;

namespace WatchList.Core.Tools.Concrete.Validations.Role;

public class CreateRoleValidation : AbstractValidator<CreateRoleDto>
{
    public CreateRoleValidation()
    {
        RuleFor(x => x.RoleName)
            .NotEmpty().WithMessage("Rol adı alanı zorunludur")
            .Length(2, 50).WithMessage("Rol adı 2 ile 50 karakter arasında olmalıdır");

        RuleFor(x => x.Description)
            .MaximumLength(200).WithMessage("Açıklama 200 karakterden uzun olamaz")
            .When(x => !string.IsNullOrEmpty(x.Description));
    }
}