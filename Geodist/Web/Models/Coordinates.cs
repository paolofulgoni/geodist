namespace Geodist.Web.Models;

/// <summary>
/// Geographical coordinates of a point
/// </summary>
/// <param name="Latitude">Latitude in degrees</param>
/// <param name="Longitude">Longitude in degrees</param>
public record Coordinates(double Latitude, double Longitude);
