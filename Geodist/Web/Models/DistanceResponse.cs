namespace Geodist.Web.Models;

/// <summary>
/// Response of the Distance endpoint
/// </summary>
/// <param name="Distance">Computed distance</param>
/// <param name="Unit">Distance unit (km or mi)</param>
public record DistanceResponse(double Distance, string Unit);
