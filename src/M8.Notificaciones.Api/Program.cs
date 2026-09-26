using Microsoft.OpenApi.Models;

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

var app = builder.Build();

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
