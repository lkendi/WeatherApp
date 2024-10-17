FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5285

ENV ASPNETCORE_URLS=http://+:5285

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src

COPY ["WeatherApp.MVC/WeatherApp.MVC.csproj", "WeatherApp.MVC/"]
COPY ["WeatherApp.Application/WeatherApp.Application.csproj", "WeatherApp.Application/"]
COPY ["WeatherApp.Domain/WeatherApp.Domain.csproj", "WeatherApp.Domain/"]
COPY ["WeatherApp.Infrastructure/WeatherApp.Infrastructure.csproj", "WeatherApp.Infrastructure/"]

RUN dotnet restore "WeatherApp.MVC/WeatherApp.MVC.csproj"

COPY . .
WORKDIR "/src/WeatherApp.MVC"
RUN dotnet build "WeatherApp.MVC.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "WeatherApp.MVC.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WeatherApp.MVC.dll"]
