using FluentValidation;

namespace BusDelivery.Contract.Services.V1.Coordinate.Validators;
public class CreateCoordinateValidator : AbstractValidator<Command.CreateCoordinateCommand>
{
    public CreateCoordinateValidator()
    {
        RuleFor(x => x.lat).NotEmpty().WithMessage("Lat is required.");
        RuleFor(x => x.lng).NotEmpty().WithMessage("Lng is required.");
        RuleFor(x => x.stt).NotEmpty().WithMessage("Stt is required.");
        RuleFor(x => x.routeId).NotEmpty().WithMessage("RouteId is required");
    }
}
