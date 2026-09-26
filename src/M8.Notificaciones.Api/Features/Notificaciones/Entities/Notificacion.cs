using M8.Notificaciones.Api.Features.Notificaciones.Dtos;

namespace M8.Notificaciones.Api.Features.Notificaciones.Entities;

public class Notificacion
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Destinatario { get; set; } = string.Empty;
    public CanalNotificacion Canal { get; set; }
    public string Asunto { get; set; } = string.Empty;
    public string Mensaje { get; set; } = string.Empty;
    public string? ReferenciaId { get; set; }
    public EstadoNotificacion Estado { get; set; } = EstadoNotificacion.Pendiente;
    public int IntentosReenvio { get; set; } = 0;
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    public DateTime? FechaEnvio { get; set; }
    public string? ErrorUltimoIntento { get; set; }
}
