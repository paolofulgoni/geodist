using Geodist.Domain;
using Geodist.Web;
using Geodist.Web.Models;
using Geodist.Web.Validators;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Geodist.UnitTests.Web;

public class DistanceEndpointTests
{
    [Fact]
    public void ComputeDistance_WhenValidParameters_ReturnsOkResult()
    {
        // arrange
        var request = new DistanceRequest(
            new Coordinates(0, 0),
            new Coordinates(0, 0));
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
