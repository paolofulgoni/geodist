namespace Geodist.Domain;

public class CosineLawDistanceCalculator : IGeographicalDistanceCalculator
{
    private const double _earthRadius = 6371;
    
    public double Distance(double pointALatitude, double pointALongitude, double pointBLatitude, double pointBLongitude)
    {
        double a = DegToRad(90.0 - pointBLatitude);
        double b = DegToRad(90.0 - pointALatitude);
        double phi = DegToRad(pointALongitude - pointBLongitude);

        double p = Math.Acos(Math.Cos(a) * Math.Cos(b) + Math.Sin(a) * Math.Sin(b) * Math.Cos(phi));

        return p * _earthRadius;
    }
    
    private static double DegToRad(double angleDeg) => angleDeg * Math.PI / 180.0;
}
