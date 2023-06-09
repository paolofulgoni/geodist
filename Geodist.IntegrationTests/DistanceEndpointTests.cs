using FluentAssertions;
using Geodist.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Net.Http.Headers;
using System.Net;
using System.Net.Http.Json;

namespace Geodist.IntegrationTests;

public class DistanceEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient;

    public DistanceEndpointTests(WebApplicationFactory<Program> webApplicationFactory)
    {
        _httpClient = webApplicationFactory.CreateClient();
        _httpClient.DefaultRequestHeaders.Add(HeaderNames.AcceptLanguage, "en-IE");
    }
    
    [Theory]
    [InlineData(-90.1, 0, 0, 0)]
    [InlineData(90.1, 0, 0, 0)]
    [InlineData(0, -180.1, 0, 0)]
    [InlineData(0, 180.1, 0, 0)]
    [InlineData(0, 0, -90.1, 0)]
    [InlineData(0, 0, 90.1, 0)]
    [InlineData(0, 0, 0, -180.1)]
    [InlineData(0, 0, 0, 180.1)]
    public async Task PostDistanceWithValidationProblems(
        double pointALatitude,
        double pointALongitude,
        double pointBLatitude,
        double pointBLongitude)
    {
        // arrange
        var request = new DistanceRequest(
            new Coordinates(pointALatitude, pointALongitude),
            new Coordinates(pointBLatitude, pointBLongitude));
        
        // act
        var response = await _httpClient.PostAsJsonAsync("/distance", request);

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var problemResult = await response.Content.ReadFromJsonAsync<HttpValidationProblemDetails>();
        problemResult!.Errors.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task PostDistanceWithValidParameters()
    {
        // arrange
        var request = new DistanceRequest(
            new Coordinates(0.0, 0.0),
            new Coordinates(0.0, 0.0));
        
        // act
        var response = await _httpClient.PostAsJsonAsync("/distance", request);

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var distanceResponse = await response.Content.ReadFromJsonAsync<DistanceResponse>();
        distanceResponse!.Distance.Should().Be(0.0);
        distanceResponse.Unit.Should().Be("km");
    }
}
