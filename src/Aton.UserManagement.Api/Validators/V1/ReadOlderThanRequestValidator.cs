using Aton.UserManagement.Api.Requests.V1;
using FluentValidation;

namespace Aton.UserManagement.Api.Validators.V1;

public class ReadOlderThanRequestValidator : AbstractValidator<ReadOlderThanRequest>
{
    public ReadOlderThanRequestValidator()
    {
        var msg = "Ошибка в поле {PropertyName}: значение {PropertyValue}";

        RuleFor(x => x.Age)
            .GreaterThanOrEqualTo(0)
            .WithMessage(msg);
    }
}