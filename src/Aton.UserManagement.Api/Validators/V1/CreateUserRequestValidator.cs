using Aton.UserManagement.Api.Requests.V1;
using FluentValidation;

namespace Aton.UserManagement.Api.Validators.V1;

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
// TODO добавить валидаторы на все (на основе того, что написано в тз)
    public CreateUserRequestValidator()
    {
        // RuleFor(x => x.UserId)
        //     .GreaterThan(0);
        //
        // RuleFor(x => x.Goods)
        //     .NotEmpty();
        //
        // RuleForEach(x => x.Goods)
        //     .SetValidator(new CalculateRequestGoodPropertiesValidator());
    }
}