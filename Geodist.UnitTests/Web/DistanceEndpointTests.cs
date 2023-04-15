using Geodist.Domain;
using Geodist.Web;
using Geodist.Web.Models;
using Geodist.Web.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Localization;

namespace Geodist.UnitTests.Web;

public class DistanceEndpointTests
{
    [Theory]
    [InlineData(53.3409518, -6.2678073, 42.3514956, -71.0611723, "en-IE", 4809.4, "km")]
    [InlineData(53.3409518, -6.2678073, 42.3514956, -71.0611723, "en-US", 2988.4, "mi")]
    public void ComputeDistance_WhenValidParameters_ReturnsOkResultWithCorrectLocale(
        double pointALatitude,
        double pointALongitude,
        double pointBLatitude,
        double pointBLongitude,
        string locale,
        double expectedDistance,
        string expectedUnit)
    {
        // arrange
        var request = new DistanceRequest(
            new Coordinates(pointALatitude, pointALongitude),
            new Coordinates(pointBLatitude, pointBLongitude));
        var validator = new DistanceRequestValidator();
        var distanceCalculatorFactory = new GeographicalDistanceCalculatorFactory();
        var context = new DefaultHttpContext();
        context.Features.Set<IRequestCultureFeature>(new RequestCultureFeature(new RequestCulture(locale), null));

        // act
        var response = DistanceEndpoint.ComputeDistance(request, validator, distanceCalculatorFactory, context);

        // assert
        response.Result.Should().BeOfType<Ok<DistanceResponse>>();
        response.Result.As<Ok<DistanceResponse>>().Value!.Distance.Should()
            .BeApproximately(expectedDistance, expectedDistance * 0.0001);
        response.Result.As<Ok<DistanceResponse>>().Value!.Unit.Should().Be(expectedUnit);
    }

    [Theory]
    [InlineData(-90.1, 0, 0, 0)]
    [InlineData(90.1, 0, 0, 0)]
    [InlineData(0, -180.1, 0, 0)]
    [InlineData(0, 180.1, 0, 0)]
    [InlineData(0, 0, -90.1, 0)]
    [InlineData(0, 0, 90.1, 0)]
    [InlineData(0, 0, 0, -180.1)]
    [InlineData(0, 0, 0, 180.1)]
    public void ComputeDistance_WhenArgumentsAreOutOfRange_ReturnsValidationProblem(
        double pointALatitude,
        double pointALongitude,
        double pointBLatitude,
        double pointBLongitude)
    {
        // arrange
        var request = new DistanceRequest(
            new Coordinates(pointALatitude, pointALongitude),
            new Coordinates(pointBLatitude, pointBLongitude));
        var validator = new DistanceRequestValidator();
        var distanceCalculatorFactory = new GeographicalDistanceCalculatorFactory();
        var context = new DefaultHttpContext();

        // act
        var response = DistanceEndpoint.ComputeDistance(request, validator, distanceCalculatorFactory, context);

        // assert
        response.Result.Should().BeOfType<ValidationProblem>();
    }
}
