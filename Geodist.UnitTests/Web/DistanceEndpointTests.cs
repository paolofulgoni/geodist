using Geodist.Domain;
using Geodist.Web;
using Geodist.Web.Models;
using Geodist.Web.Validators;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Geodist.UnitTests.Web;

public class DistanceEndpointTests
{
    [Theory]
    [InlineData(53.3409518, -6.2678073, 42.3514956, -71.0611723, 4809.4)]
    [InlineData(42.3514956, -71.0611723, 53.3409518, -6.2678073, 4809.4)]
    [InlineData(50.13397, 8.56566, 53.3409518, -6.2678073, 1079.4)]
    [InlineData(53.3409518, -6.2678073, 50.13397, 8.56566, 1079.4)]
    [InlineData(-34.3567014, 18.4745813, 47.7995985, -73.5394652, 12892.8)]
    [InlineData(47.7995985, -73.5394652, -34.3567014, 18.4745813, 12892.8)]
    [InlineData(22.123, 131.123, -87.123, -171.123, 12294.6)]
    [InlineData(-87.123, -171.123, 22.123, 131.123, 12294.6)]
    [InlineData(0.0, -180.0, 0.0, 180.0, 0)]
    [InlineData(90.0, 12.0, 90.0, -12.0, 0)]
    [InlineData(-90.0, 12.0, -90.0, -12.0, 0)]
    public void ComputeDistance_WhenDifferentPositions_ReturnsOkResultWithCorrectDistance(
        double pointALatitude,
        double pointALongitude,
        double pointBLatitude,
        double pointBLongitude,
        double expectedDistance)
    {
        // arrange
        var request = new DistanceRequest(
            new Coordinates(pointALatitude, pointALongitude),
            new Coordinates(pointBLatitude, pointBLongitude));
        var validator = new DistanceRequestValidator();
        var distanceCalculatorFactory = new GeographicalDistanceCalculatorFactory();

        // act
        var response = DistanceEndpoint.ComputeDistance(request, validator, distanceCalculatorFactory);

        // assert
        response.Result.Should().BeOfType<Ok<DistanceResponse>>();
        response.Result.As<Ok<DistanceResponse>>().Value!.Distance.Should()
            .BeApproximately(expectedDistance, expectedDistance * 0.0001);
    }

    [Theory]
    [InlineData(53.3409518, -6.2678073)]
    [InlineData(-34.3567014, 18.4745813)]
    public void ComputeDistance_WhenEqualPosition_ReturnsOkResultWithZeroDistance(
        double pointLatitude,
        double pointLongitude)
    {
        // arrange
        var request = new DistanceRequest(
            new Coordinates(pointLatitude, pointLongitude),
            new Coordinates(pointLatitude, pointLongitude));
        var validator = new DistanceRequestValidator();
        var distanceCalculatorFactory = new GeographicalDistanceCalculatorFactory();


        // act
        var response = DistanceEndpoint.ComputeDistance(request, validator, distanceCalculatorFactory);

        // assert
        response.Result.Should().BeOfType<Ok<DistanceResponse>>();
        response.Result.As<Ok<DistanceResponse>>().Value!.Distance.Should().Be(0.0);
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

        // act
        var response = DistanceEndpoint.ComputeDistance(request, validator, distanceCalculatorFactory);

        // assert
        response.Result.Should().BeOfType<ValidationProblem>();
    }
}
