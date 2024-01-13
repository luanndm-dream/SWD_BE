using FluentValidation;

namespace BusDelivery.Contract.Services.V1.Office.Validators;
public class GetOfficeByIdValidator : AbstractValidator<Query.GetOfficeByIdQuery>
{
    public GetOfficeByIdValidator()
    {
        RuleFor(x => x.officeId)
            .NotNull().WithMessage("officeId is not null");
    }
}
