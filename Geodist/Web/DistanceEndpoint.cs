using FluentValidation;
using Geodist.Domain;
using Geodist.Web.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Geodist.Web;

public static class DistanceEndpoint
{
    private const string kilometersUnit = "km";
    private const string MilesUnit = "mi";

    public static Results<Ok<DistanceResponse>, ValidationProblem> ComputeDistance(
        [FromBody] DistanceRequest request,
        IValidator<DistanceRequest> requestValidator,
        IGeographicalDistanceCalculatorFactory distanceCalculatorFactory,
        HttpContext context)
    {
        var validationResult = requestValidator.Validate(request);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(validationResult.ToDictionary());
        }

        var distanceCalculator = distanceCalculatorFactory.Create(GeographicalDistanceMethod.CosineLaw);

        double distance = distanceCalculator.Distance(
            request.PointA.Latitude,
            request.PointA.Longitude,
            request.PointB.Latitude,
            request.PointB.Longitude);

        var requestCultureFeature = context.Features.Get<IRequestCultureFeature>();
        var culture = requestCultureFeature?.RequestCulture.Culture ?? CultureInfo.InvariantCulture;
        var region = new RegionInfo(culture.Name);

        if (!region.IsMetric)
        {
            return TypedResults.Ok(new DistanceResponse(DistanceUnitConverter.KilometersToMiles(distance), MilesUnit));
        }

        return TypedResults.Ok(new DistanceResponse(distance, kilometersUnit));
    }
}
