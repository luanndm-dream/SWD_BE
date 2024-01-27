using FluentValidation;

namespace BusDelivery.Contract.Services.V1.Office.Validators;
public class UpdateOfficeValidator : AbstractValidator<Command.UpdateOfficeCommand>
{
    public UpdateOfficeValidator()
    {
        RuleFor(x => x.Id).NotNull().WithMessage("Id is required");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required.");
        RuleFor(x => x.Lat).NotEmpty().WithMessage("Lat is required.");
        RuleFor(x => x.Lng).NotEmpty().WithMessage("Lng is required.");
        RuleFor(x => x.Image).NotEmpty().WithMessage("Image is required.");
        RuleFor(x => x.IsActive).NotNull().WithMessage("Status is required");
    }
}
