using FluentValidation;

namespace BusDelivery.Contract.Services.V1.Authentication.Validators;
public class RegisterValidator : AbstractValidator<Command.RegisterCommand>
{
    public RegisterValidator()
    {
        RuleFor(x => x.roleId).NotNull().WithMessage("Role is required");
        RuleFor(x => x.officeId).NotNull().WithMessage("Office is required");
        RuleFor(x => x.name)
             .NotEmpty()
             .MinimumLength(3).WithMessage("Name must be at least 3 characters long.")
             .Matches("^[a-zA-Z0-9_]*$").WithMessage("Name may only contain alphanumeric characters and underscores.");

        RuleFor(x => x.email)
            .NotEmpty()
            .EmailAddress().WithMessage("Invalid email address format.");

        RuleFor(x => x.password)
            .NotEmpty()
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one non-alphanumeric character.");

        RuleFor(x => x.phoneNumber).NotEmpty()
            .Matches(@"^[0-9]+$").WithMessage("Invalid phone number format.");

        RuleFor(x => x.gentle)
            .NotEmpty()
            .IsInEnum()
            .WithMessage("Invalid Gentle value. Accepted values are 'Male', 'Female', 'Others'.");

        RuleFor(x => x.deviceId).NotEmpty().WithMessage("DeviceId is required");
        RuleFor(x => x.deviceVersion).NotEmpty().WithMessage("DeviceVersion is required");
        RuleFor(x => x.OS).NotEmpty().WithMessage("OS is required");
        RuleFor(x => x.IsDeleted).NotEmpty().WithMessage("IsDeleted is required");
        RuleFor(x => x.IsActive).NotEmpty().WithMessage("IsActive is required");
    }
}
