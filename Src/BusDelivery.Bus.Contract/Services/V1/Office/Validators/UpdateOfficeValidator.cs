using FluentValidation;

namespace BusDelivery.Contract.Services.V1.Office.Validators;
public class UpdateOfficeValidator : AbstractValidator<Command.UpdateOfficeCommand>
{
    public UpdateOfficeValidator()
    {
        RuleFor(x => x.id).NotNull().WithMessage("Id is required");
        RuleFor(x => x.name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.address).NotEmpty().WithMessage("Address is required.");
        RuleFor(x => x.lat).NotEmpty().WithMessage("Lat is required.");
        RuleFor(x => x.lng).NotEmpty().WithMessage("Lng is required.");
        RuleFor(x => x.image).NotEmpty().WithMessage("Image is required.");
        RuleFor(x => x.status).NotNull().WithMessage("Status is required");
    }
}
