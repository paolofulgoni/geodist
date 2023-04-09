using Geodist.Domain;

namespace Geodist.UnitTests.Domain;

public class CosineLawDistanceCalculatorTests
{
    [Theory]
    [InlineData(53.3409518, -6.2678073, 42.3514956, -71.0611723, 4809.4)]
    [InlineData(42.3514956, -71.0611723, 53.3409518, -6.2678073, 4809.4)]
    [InlineData(50.13397, 8.56566, 53.3409518, -6.2678073, 1079.4)]
    [InlineData(53.3409518, -6.2678073, 50.13397, 8.56566, 1079.4)]
    [InlineData(-34.3567014, 18.4745813, 47.7995985, -73.5394652, 12892.8)]
    [InlineData(47.7995985, -73.5394652, -34.3567014, 18.4745813,12892.8)]
    [InlineData(22.123, 131.123, -87.123, -171.123,12294.6)]
    [InlineData(-87.123, -171.123,22.123, 131.123, 12294.6)]
    [InlineData(0.0, -180.0,0.0, 180.0, 0)]
    [InlineData(90.0, 12.0,90.0, -12.0, 0)]
    [InlineData(-90.0, 12.0,-90.0, -12.0, 0)]
    public void Distance_WhenDifferentPositions_ReturnsCorrectDistance(
        double pointALatitude,
        double pointALongitude,
        double pointBLatitude,
        double pointBLongitude,
        double expectedDistance)
    {
        // arrange
        var uut = new CosineLawDistanceCalculator();

        // act
        var distance = uut.Distance(pointALatitude, pointALongitude, pointBLatitude, pointBLongitude);

        // assert
        distance.Should().BeApproximately(expectedDistance, expectedDistance * 0.0001);
    }
    
    [Theory]
    [InlineData(53.3409518, -6.2678073)]
    [InlineData(-34.3567014, 18.4745813)]
    public void Distance_WhenEqualPosition_ReturnsZero(
        double pointLatitude,
        double pointLongitude)
    {
        // arrange
        var uut = new CosineLawDistanceCalculator();

        // act
        var distance = uut.Distance(pointLatitude, pointLongitude, pointLatitude, pointLongitude);

        // assert
        distance.Should().Be(0);
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
    public void Distance_WhenArgumentsAreOutOfRange_ThrowsArgumentException(
        double pointALatitude,
        double pointALongitude,
        double pointBLatitude,
        double pointBLongitude)
    {
        // arrange
        var uut = new CosineLawDistanceCalculator();

        // act
        var act = () => uut.Distance(pointALatitude, pointALongitude, pointBLatitude, pointBLongitude);

        act.Should().Throw<ArgumentException>();
    }
}
