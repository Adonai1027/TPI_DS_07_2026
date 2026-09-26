namespace M8.Notificaciones.Api.Features.Notificaciones.Dtos;

public enum CanalNotificacion
{
    Email,
    Push,
    Sms
}

public enum EstadoNotificacion
{
    Pendiente,
    Enviado,
    Fallido
}

public record EnviarNotificacionRequest(
    string Destinatario,
    CanalNotificacion Canal,
    string Asunto,
    string Mensaje,
    string? ReferenciaId = null
);

public record NotificacionDetalleResponse(
    Guid Id,
    string Destinatario,
    CanalNotificacion Canal,
    string Asunto,
    EstadoNotificacion Estado,
    DateTime FechaCreacion,
    DateTime? FechaEnvio,
    int IntentosReenvio
);
