namespace Geodist.Domain;

public class CosineLawDistanceCalculator : IGeographicalDistanceCalculator
{
    private const double _earthRadius = 6371;
    
    public double Distance(double pointALatitude, double pointALongitude, double pointBLatitude, double pointBLongitude)
    {
        if (Math.Abs(pointALatitude) > 90.0) throw new ArgumentOutOfRangeException(nameof(pointALatitude));
        if (Math.Abs(pointALongitude) > 180.0) throw new ArgumentOutOfRangeException(nameof(pointALongitude));
        if (Math.Abs(pointBLatitude) > 90.0) throw new ArgumentOutOfRangeException(nameof(pointBLatitude));
        if (Math.Abs(pointBLongitude) > 180.0) throw new ArgumentOutOfRangeException(nameof(pointBLongitude));
        
        // optimization in case point A and point B are equal
        if (pointALatitude == pointBLatitude && pointALongitude == pointBLongitude)
        {
            return 0;
        }
        
        double a = DegToRad(90.0 - pointBLatitude);
        double b = DegToRad(90.0 - pointALatitude);
        double phi = DegToRad(pointALongitude - pointBLongitude);

        double p = Math.Acos(Math.Cos(a) * Math.Cos(b) + Math.Sin(a) * Math.Sin(b) * Math.Cos(phi));

        return p * _earthRadius;
    }
    
    private static double DegToRad(double angleDeg) => angleDeg * Math.PI / 180.0;
}
