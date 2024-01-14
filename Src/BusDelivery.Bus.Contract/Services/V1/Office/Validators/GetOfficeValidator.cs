using FluentValidation;

namespace BusDelivery.Contract.Services.V1.Office.Validators;
public class GetOfficeValidator : AbstractValidator<Query.GetOfficeQuery>
{
    public GetOfficeValidator()
    {
        RuleFor(x => x.pageSize)
            .GreaterThan(0).WithMessage("pageIndex is integer greater than 0");
        RuleFor(x => x.pageSize)
            .GreaterThan(0).WithMessage("pageSize is integer greater than 0 and less than or equal to 10")
            .LessThanOrEqualTo(10).WithMessage("pageSize is integer greater than 0 and less than or equal to 10");
    }
}
