using Geodist.Web.Model;

namespace Geodist.Web;

public static class DistanceEndpoint
{
    public static RouteHandlerBuilder MapDistanceEndpoint(this WebApplication app)
    {
        return app.MapPost("/distance",
            (DistanceRequest request) => TypedResults.Ok(new DistanceResponse(1.2)));
    }
}
