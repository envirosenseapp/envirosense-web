FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY . .
RUN dotnet restore EnviroSense.API/EnviroSense.API.csproj

WORKDIR /src/EnviroSense.API
RUN dotnet publish "EnviroSense.API.csproj" -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

COPY --from=build /app .

EXPOSE 8080

ENTRYPOINT ["dotnet", "EnviroSense.API.dll"]
