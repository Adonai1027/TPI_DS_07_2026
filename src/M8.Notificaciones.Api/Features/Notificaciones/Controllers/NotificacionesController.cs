using Microsoft.AspNetCore.Mvc;
using M8.Notificaciones.Api.Features.Notificaciones.Dtos;

namespace M8.Notificaciones.Api.Features.Notificaciones.Controllers;

[ApiController]
[Route("api/[controller]")]
[Tags("Notificaciones (Célula 1)")]
public class NotificacionesController : ControllerBase
{
    private readonly ILogger<NotificacionesController> _logger;

    public NotificacionesController(ILogger<NotificacionesController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Envía una notificación directa o manual (Email, Push, SMS)
    /// </summary>
    [HttpPost("enviar")]
    public ActionResult<NotificacionDetalleResponse> Enviar([FromBody] EnviarNotificacionRequest request)
    {
        _logger.LogInformation("Recibida solicitud de notificación para {Destinatario} vía {Canal}", request.Destinatario, request.Canal);

        var respuesta = new NotificacionDetalleResponse(
            Id: Guid.NewGuid(),
            Destinatario: request.Destinatario,
            Canal: request.Canal,
            Asunto: request.Asunto,
            Estado: EstadoNotificacion.Enviado,
            FechaCreacion: DateTime.UtcNow,
            FechaEnvio: DateTime.UtcNow,
            IntentosReenvio: 0
        );

        return Ok(respuesta);
    }

    /// <summary>
    /// Consulta el estado y seguimiento de entrega de una notificación (RF-8.6)
    /// </summary>
    [HttpGet("{id:guid}")]
    public ActionResult<NotificacionDetalleResponse> ObtenerPorId(Guid id)
    {
        var mock = new NotificacionDetalleResponse(
            Id: id,
            Destinatario: "usuario@ejemplo.com",
            Canal: CanalNotificacion.Email,
            Asunto: "Tu conductor está en camino",
            Estado: EstadoNotificacion.Enviado,
            FechaCreacion: DateTime.UtcNow.AddMinutes(-5),
            FechaEnvio: DateTime.UtcNow.AddMinutes(-4),
            IntentosReenvio: 0
        );

        return Ok(mock);
    }
}
