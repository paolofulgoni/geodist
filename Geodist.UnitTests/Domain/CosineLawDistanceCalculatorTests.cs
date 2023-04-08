using Geodist.Domain;

namespace Geodist.UnitTests.Domain;

public class CosineLawDistanceCalculatorTests
{
    [Fact]
    public void Distance_ReturnsValue_WhenParametersAreValid()
    {
        // arrange
        var uut = new CosineLawDistanceCalculator();

        // act
        var distance = uut.Distance(0, 0, 0, 0);

        // assert
        distance.Should().Be(5584);
    }
}
