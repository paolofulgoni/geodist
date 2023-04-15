_Geodist, a sample .NET 7 Minimal API project to compute geographical distance_

# Geodist API

[![build](https://github.com/paolofulgoni/geodist/actions/workflows/build.yml/badge.svg?branch=main)](https://github.com/paolofulgoni/geodist/actions/workflows/build.yml?query=branch%3Amain)

This REST API provides a single endpoints to get the geographical distance between two points:

```
POST /distance HTTP/1.1
Accept: application/json
Content-Type: application/json

{
  "pointA": {
    "latitude": 53.297975,
    "longitude": -6.372663
  },
  "pointB": {
    "latitude": 41.385101,
    "longitude": -81.440440
  }
}
```

```json
{
  "distance": 5536.338682266685,
  "unit": "km"
}
```

## 🎠 Run

You can easily run the service from your computer, but you'll have to compile it first. Therefore, you need to:

* Install the [.NET 7 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
* Clone the repository locally

Then use the dotnet CLI to run the service. Make sure you're on the root folder of the project, then type:

```shell
dotnet run --project ./Geodist
```

This will use the `Development` Hosting environment, therefore you can open a browser to http://localhost:5072/swagger and have fun with the Swagger UI.

Press `CTRL+C` when you're done.

## 🔧 Build and test

Make sure [.NET 7 SDK](https://dotnet.microsoft.com/download/dotnet/7.0) is installed on your dev environment. Then just open the project with your IDE of choice.

If you want to build the project using the .NET CLI, run the following command from the project's root folder:

```shell
dotnet build
```

The project contains some unit and integration tests. You can run them with the following command:

```shell
dotnet test
```

## 🌎 Localisation

Based on the request locale, a different measuring unit is used (i.e. kilometers or miles).

The locale is determined by the default .NET 7 strategies (see [docs](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/localization/select-language-culture?view=aspnetcore-7.0)).
This includes the `Accept-Language` header, which is used in tests.

## ➕Add other distance computation methods 

Different approaches exists to calculate the distance.
For example ignoring the sphericity of the earth and applying Pythagoras can be a method by approximation or considering the earth as ellipsoid is another method.

The code uses the strategy pattern as is open to extension, by adding a new `IGeographicalDistanceCalculator` implementation and adding it to `GeographicalDistanceCalculatorFactory`.
