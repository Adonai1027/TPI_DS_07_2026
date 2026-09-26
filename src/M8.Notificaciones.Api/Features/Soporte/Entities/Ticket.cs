using M8.Notificaciones.Api.Features.Soporte.Dtos;

namespace M8.Notificaciones.Api.Features.Soporte.Entities;

public class Ticket
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string UsuarioId { get; set; } = string.Empty;
    public string? ViajeId { get; set; }
    public TipoIncidencia Tipo { get; set; } = TipoIncidencia.Viaje;
    public string Titulo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public EstadoTicket Estado { get; set; } = EstadoTicket.Abierto;
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    public DateTime? FechaActualizacion { get; set; }
    public DateTime? FechaCierre { get; set; }
    public string? Resolucion { get; set; }
}
