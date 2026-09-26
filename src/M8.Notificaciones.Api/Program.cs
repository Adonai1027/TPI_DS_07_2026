using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using M8.Notificaciones.Api.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Módulo 8 - Notificaciones, Documentos y Soporte",
        Version = "v1",
        Description = "API del Módulo M8 para la plataforma de movilidad urbana (TPI Desarrollo de Software 2026 - Grupo 07). Cumple con OpenAPI Specification (RNF-03)."
    });
});

// Configurar Entity Framework Core con PostgreSQL
// Configurar Entity Framework Core con PostgreSQL
// Configurar Entity Framework Core con PostgreSQL
var connectionString = builder.Configuration.GetConnectionString("PostgresConnection")
    ?? throw new InvalidOperationException("No se encontró la cadena de conexión 'PostgresConnection' en la configuración.");

builder.Services.AddDbContext<M8DbContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build(); // <--- Esta es la línea que faltaba

// Aplicar migraciones automáticas al iniciar la aplicación (RNF-05)
using (var scope = app.Services.CreateScope())

{
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    try
    {
        var db = scope.ServiceProvider.GetRequiredService<M8DbContext>();
        logger.LogInformation("Verificando y aplicando migraciones pendientes de PostgreSQL...");
        db.Database.Migrate();
        logger.LogInformation("Base de datos de M8 sincronizada correctamente.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Error al conectar o aplicar migraciones a PostgreSQL. Asegúrate de que el contenedor de Docker 'm8-postgres' esté corriendo.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "M8 API v1");
        c.RoutePrefix = string.Empty; // Carga Swagger en http://localhost:PORT/ directamente
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
