FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY . .
RUN dotnet restore EnviroSense.Web/EnviroSense.Web.csproj

WORKDIR /src/EnviroSense.Web
RUN dotnet publish "EnviroSense.Web.csproj" -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

COPY --from=build /app .

EXPOSE 8080

ENTRYPOINT ["dotnet", "EnviroSense.Web.dll"]
