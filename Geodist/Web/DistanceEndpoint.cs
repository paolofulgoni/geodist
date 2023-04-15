using FluentValidation;
using Geodist.Domain;
using Geodist.Web.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Geodist.Web;

public static class DistanceEndpoint
{
    public static Results<Ok<DistanceResponse>, ValidationProblem> ComputeDistance(
        [FromBody] DistanceRequest request,
        [FromServices] IValidator<DistanceRequest> requestValidator,
        [FromServices] IGeographicalDistanceCalculatorFactory distanceCalculatorFactory)
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

        return TypedResults.Ok(new DistanceResponse(distance));
    }
}
