namespace Geodist.Domain;

public interface IGeographicalDistanceCalculator
{
    /// <summary>
    /// Computes the geographical distance between two points A and B
    /// </summary>
    /// <param name="pointALatitude">Point A latitude in degrees</param>
    /// <param name="pointALongitude">Point A longitude in degrees</param>
    /// <param name="pointBLatitude">Point B latitude in degrees</param>
    /// <param name="pointBLongitude">Point B longitude in degrees</param>
    /// <returns>Distance in kilometers</returns>
    double Distance(double pointALatitude, double pointALongitude, double pointBLatitude, double pointBLongitude);
}
