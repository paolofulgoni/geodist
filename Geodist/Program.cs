using FluentValidation;
using Geodist.Domain;
using Geodist.Web;
using Geodist.Web.Models;
using Geodist.Web.Validators;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IGeographicalDistanceCalculator, CosineLawDistanceCalculator>();
builder.Services.AddSingleton<IValidator<DistanceRequest>, DistanceRequestValidator>();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/distance", DistanceEndpoint.ComputeDistance)
    .WithOpenApi();

app.Run();
