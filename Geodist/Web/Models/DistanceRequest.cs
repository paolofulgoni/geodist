namespace Geodist.Web.Models;

/// <summary>
/// Request of the Distance endpoint
/// </summary>
/// <param name="PointA">First point for distance computation</param>
/// <param name="PointB">Second point for distance computation</param>
public record DistanceRequest(
    Coordinates PointA,
    Coordinates PointB
);
