using Aton.UserManagement.Api.Requests.V1;
using FluentValidation;

namespace Aton.UserManagement.Api.Validators.V1;

public class UpdateUserInfoRequestValidator : AbstractValidator<UpdateUserInfoRequest>
{
    public UpdateUserInfoRequestValidator()
    {
        var msg = "Ошибка в поле {PropertyName}: значение {PropertyValue}";

        RuleFor(x => x.UserLogin)
            .Length(1, 30)
            .Matches(@"^[a-zA-Z0-9]+$")
            .WithMessage(msg);

        RuleFor(x => x.Name)
            .Length(1, 30)
            .Matches(@"^[a-zA-Zа-яА-Я]+$")
            .WithMessage(msg);

        RuleFor(x => x.Gender)
            .InclusiveBetween(0, 1)
            .WithMessage(msg);
    }
}