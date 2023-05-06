using Aton.UserManagement.Api.Requests.V1;
using FluentValidation;

namespace Aton.UserManagement.Api.Validators.V1;

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        var msg = "Ошибка в поле {PropertyName}: значение {PropertyValue}";

        RuleFor(x => x.UserToCreate.Login)
            .Length(1, 30)
            .Matches(@"^[a-zA-Z0-9]+$")
            .WithMessage(msg);

        RuleFor(x => x.UserToCreate.Password)
            .Length(1, 30)
            .Matches(@"^[a-zA-Z0-9]+$")
            .WithMessage(msg);

        RuleFor(x => x.UserToCreate.Name)
            .Length(1, 30)
            .Matches(@"^[a-zA-Zа-яА-Я]+$")
            .WithMessage(msg);

        RuleFor(x => x.UserToCreate.Gender)
            .InclusiveBetween(0, 1)
            .WithMessage(msg);
    }
}