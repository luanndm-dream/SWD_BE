using FluentValidation;

namespace BusDelivery.Contract.Services.V1.Office.Validators;
public class CreateOfficeValidator : AbstractValidator<Command.CreateOfficeCommand>
{
    public CreateOfficeValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required.");
        RuleFor(x => x.Lat).NotEmpty().WithMessage("Lat is required.");
        RuleFor(x => x.Lng).NotEmpty().WithMessage("Lng is required.");
        RuleFor(x => x.Image).NotEmpty().WithMessage("Image is required.");
        RuleFor(x => x.IsActive).NotEmpty().WithMessage("Status is required");
    }
}
