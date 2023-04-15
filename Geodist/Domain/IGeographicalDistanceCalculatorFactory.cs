namespace Geodist.Domain;

public interface IGeographicalDistanceCalculatorFactory
{
    IGeographicalDistanceCalculator Create(GeographicalDistanceMethod computationMethod);
}
