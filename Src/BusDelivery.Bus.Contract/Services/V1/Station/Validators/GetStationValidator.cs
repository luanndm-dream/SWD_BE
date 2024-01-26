using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace BusDelivery.Contract.Services.V1.Station.Validators;
public class GetStationValidator : AbstractValidator<Query.GetStation>
{
    public GetStationValidator()
    {
        RuleFor(x => x.pageSize)
           .GreaterThan(0).WithMessage("pageIndex is integer greater than 0");
        RuleFor(x => x.pageSize)
            .GreaterThan(0).WithMessage("pageSize is integer greater than 0 and less than or equal to 10")
            .LessThanOrEqualTo(10).WithMessage("pageSize is integer greater than 0 and less than or equal to 10");
    }
}
