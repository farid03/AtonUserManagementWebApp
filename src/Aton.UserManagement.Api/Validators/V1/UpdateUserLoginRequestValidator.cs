using Aton.UserManagement.Api.Requests.V1;
using FluentValidation;

namespace Aton.UserManagement.Api.Validators.V1;

public class UpdateUserLoginRequestValidator : AbstractValidator<UpdateUserLoginRequest>
{
    public UpdateUserLoginRequestValidator()
    {
        var msg = "Ошибка в поле {PropertyName}: значение {PropertyValue}";
        
        RuleFor(x => x.NewLogin)
            .Length(1, 30)
            .Matches(@"^[a-zA-Z0-9]+$")
            .WithMessage(msg);
    }
}