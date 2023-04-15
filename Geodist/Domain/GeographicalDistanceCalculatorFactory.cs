namespace Geodist.Domain;

public class GeographicalDistanceCalculatorFactory : IGeographicalDistanceCalculatorFactory
{
    public IGeographicalDistanceCalculator Create(GeographicalDistanceMethod computationMethod) =>
        computationMethod switch
        {
            GeographicalDistanceMethod.CosineLaw => new CosineLawDistanceCalculator(),
            _ => throw new ArgumentOutOfRangeException(nameof(computationMethod), computationMethod,
                "Unsupported computation method")
        };
}
