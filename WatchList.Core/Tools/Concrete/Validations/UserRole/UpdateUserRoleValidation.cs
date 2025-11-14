using FluentValidation;
using WatchList.Core.Tools.Concrete.Dto.UserRole.Request;

namespace WatchList.Core.Tools.Concrete.Validations.UserRole;

public class UpdateUserRoleValidation : AbstractValidator<UpdateUserRoleDto>
{
    public UpdateUserRoleValidation()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Geçerli bir kullanıcı-rol ID'si giriniz");

        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("Geçerli bir kullanıcı ID'si giriniz");

        RuleFor(x => x.RoleId)
            .GreaterThan(0).WithMessage("Geçerli bir rol ID'si giriniz");
    }
}
