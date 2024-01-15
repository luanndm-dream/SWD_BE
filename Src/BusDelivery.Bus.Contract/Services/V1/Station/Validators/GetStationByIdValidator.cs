using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace BusDelivery.Contract.Services.V1.Station.Validators;
public class GetStationByIdValidator : AbstractValidator<Query.GetStationById>
{
    public GetStationByIdValidator()
    {
        RuleFor(x => x.stationId)
    .NotNull().WithMessage("officeId is not null");

    }
}
