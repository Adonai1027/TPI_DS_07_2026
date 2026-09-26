namespace M8.Notificaciones.Api.Features.Soporte.Dtos;

public enum EstadoTicket
{
    Abierto,
    EnRevision,
    Resuelto,
    Cerrado
}

public enum TipoIncidencia
{
    Viaje,
    Pago,
    Conductor,
    App
}

public record CrearTicketRequest(
    string UsuarioId,
    string? ViajeId,
    TipoIncidencia Tipo,
    string Titulo,
    string Descripcion
);

public record TicketResponse(
    Guid Id,
    string UsuarioId,
    string? ViajeId,
    TipoIncidencia Tipo,
    string Titulo,
    string Descripcion,
    EstadoTicket Estado,
    DateTime FechaCreacion,
    DateTime? FechaCierre
);
