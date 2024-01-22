using FluentValidation;

namespace BusDelivery.Contract.Services.V1.Office.Validators;
public class CreateOfficeValidator : AbstractValidator<Command.CreateOfficeCommand>
{
    public CreateOfficeValidator()
    {
        RuleFor(x => x.name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.address).NotEmpty().WithMessage("Address is required.");
        RuleFor(x => x.lat).NotEmpty().WithMessage("Lat is required.");
        RuleFor(x => x.lng).NotEmpty().WithMessage("Lng is required.");
        RuleFor(x => x.image).NotEmpty().WithMessage("Image is required.");
        RuleFor(x => x.status).NotEmpty().WithMessage("Status is required");
    }
}
