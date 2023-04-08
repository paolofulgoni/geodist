using FluentValidation;
using Geodist.Web.Models;

namespace Geodist.Web.Validators;

public class DistanceRequestValidator : AbstractValidator<DistanceRequest>
{
    public DistanceRequestValidator()
    {
        RuleFor(x => x.PointA.Latitude).InclusiveBetween(-90.0, 90.0);
        RuleFor(x => x.PointA.Longitude).InclusiveBetween(-90.0, 90.0);
        RuleFor(x => x.PointB.Latitude).InclusiveBetween(-90.0, 90.0);
        RuleFor(x => x.PointB.Longitude).InclusiveBetween(-90.0, 90.0);
    }
}
