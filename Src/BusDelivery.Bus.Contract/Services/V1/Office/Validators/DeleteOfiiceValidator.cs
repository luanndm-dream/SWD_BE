using FluentValidation;

namespace BusDelivery.Contract.Services.V1.Office.Validators;
public class DeleteOfiiceValidator : AbstractValidator<Command.DeleteOfficeCommand>
{
    public DeleteOfiiceValidator()
    {
        RuleFor(x => x.id).NotNull().WithMessage("Id is required.");
    }
}
