namespace Geodist.Web.Model;

public record DistanceRequest(
    Coordinates PointA,
    Coordinates PointB
);
