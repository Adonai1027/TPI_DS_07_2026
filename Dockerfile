# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar csproj y restaurar dependencias
COPY src/M8.Notificaciones.Api/*.csproj src/M8.Notificaciones.Api/
RUN dotnet restore src/M8.Notificaciones.Api/M8.Notificaciones.Api.csproj

# Copiar el resto del código y compilar
COPY src/M8.Notificaciones.Api/ src/M8.Notificaciones.Api/
WORKDIR /app/src/M8.Notificaciones.Api
RUN dotnet publish -c Release -o /app/out

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Development

ENTRYPOINT ["dotnet", "M8.Notificaciones.Api.dll"]
