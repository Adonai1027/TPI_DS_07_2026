using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using M8.Notificaciones.Api.Features.Notificaciones.Dtos;
using M8.Notificaciones.Api.Features.Notificaciones.Entities;
using M8.Notificaciones.Api.Infrastructure.Persistence;

namespace M8.Notificaciones.Api.Features.Notificaciones.Controllers;

[ApiController]
[Route("api/[controller]")]
[Tags("Notificaciones (Célula 1)")]
public class NotificacionesController : ControllerBase
{
    private readonly M8DbContext _dbContext;
    private readonly ILogger<NotificacionesController> _logger;

    public NotificacionesController(M8DbContext dbContext, ILogger<NotificacionesController> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    /// <summary>
    /// Envía y registra una notificación directa (Email, Push, SMS)
    /// </summary>
    [HttpPost("enviar")]
    public async Task<ActionResult<NotificacionDetalleResponse>> Enviar(
        [FromBody] EnviarNotificacionRequest request, 
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Registrando notificación para {Destinatario} vía {Canal}", request.Destinatario, request.Canal);

        var notificacion = new Notificacion
        {
            Id = Guid.NewGuid(),
            Destinatario = request.Destinatario,
            Canal = request.Canal,
            Asunto = request.Asunto,
            Mensaje = request.Mensaje,
            ReferenciaId = request.ReferenciaId,
            Estado = EstadoNotificacion.Enviado, // O Pendiente según la lógica de despacho
            FechaCreacion = DateTime.UtcNow,
            FechaEnvio = DateTime.UtcNow,
            IntentosReenvio = 0
        };

        _dbContext.Notificaciones.Add(notificacion);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var respuesta = new NotificacionDetalleResponse(
            Id: notificacion.Id,
            Destinatario: notificacion.Destinatario,
            Canal: notificacion.Canal,
            Asunto: notificacion.Asunto,
            Estado: notificacion.Estado,
            FechaCreacion: notificacion.FechaCreacion,
            FechaEnvio: notificacion.FechaEnvio,
            IntentosReenvio: notificacion.IntentosReenvio
        );

        return CreatedAtAction(nameof(ObtenerPorId), new { id = notificacion.Id }, respuesta);
    }

    /// <summary>
    /// Consulta el estado y seguimiento de entrega de una notificación (RF-8.6)
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<NotificacionDetalleResponse>> ObtenerPorId(
        Guid id, 
        CancellationToken cancellationToken)
    {
        var notificacion = await _dbContext.Notificaciones
            .AsNoTracking()
            .FirstOrDefaultAsync(n => n.Id == id, cancellationToken);

        if (notificacion is null)
        {
            return NotFound(new { mensaje = $"No se encontró la notificación con Id {id}" });
        }

        var respuesta = new NotificacionDetalleResponse(
            Id: notificacion.Id,
            Destinatario: notificacion.Destinatario,
            Canal: notificacion.Canal,
            Asunto: notificacion.Asunto,
            Estado: notificacion.Estado,
            FechaCreacion: notificacion.FechaCreacion,
            FechaEnvio: notificacion.FechaEnvio,
            IntentosReenvio: notificacion.IntentosReenvio
        );

        return Ok(respuesta);
    }
}
