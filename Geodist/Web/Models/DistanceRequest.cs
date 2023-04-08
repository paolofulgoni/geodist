namespace Geodist.Web.Models;

public record DistanceRequest(
    Coordinates PointA,
    Coordinates PointB
);
