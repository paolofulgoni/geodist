using FluentValidation;
using Geodist.Domain;
using Geodist.Web;
using Geodist.Web.Models;
using Geodist.Web.Validators;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Geodist;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Geodist API", Version = "v1" });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var filePath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(filePath);
        });
        builder.Services.AddProblemDetails();
        builder.Services.AddLocalization();

        builder.Services.AddSingleton<IValidator<DistanceRequest>, DistanceRequestValidator>();
        builder.Services.AddSingleton<IGeographicalDistanceCalculatorFactory, GeographicalDistanceCalculatorFactory>();

        WebApplication app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseExceptionHandler();
        app.UseRequestLocalization();

        app.MapPost("/distance", DistanceEndpoint.ComputeDistance)
            .WithOpenApi();

        app.Run();
    }
}
