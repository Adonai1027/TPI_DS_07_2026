using Microsoft.AspNetCore.Mvc;
using M8.Notificaciones.Api.Features.Soporte.Dtos;

namespace M8.Notificaciones.Api.Features.Soporte.Controllers;

[ApiController]
[Route("api/[controller]")]
[Tags("Soporte y Tickets (Célula 3)")]
public class SoporteController : ControllerBase
{
    private readonly ILogger<SoporteController> _logger;

    public SoporteController(ILogger<SoporteController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Crea un ticket de soporte asociado a un viaje o reclamo general (RF-8.7)
    /// </summary>
    [HttpPost("tickets")]
    public ActionResult<TicketResponse> CrearTicket([FromBody] CrearTicketRequest request)
    {
        _logger.LogInformation("Creando ticket para usuario {UsuarioId}, Tipo: {Tipo}", request.UsuarioId, request.Tipo);

        var nuevoTicket = new TicketResponse(
            Id: Guid.NewGuid(),
            UsuarioId: request.UsuarioId,
            ViajeId: request.ViajeId,
            Tipo: request.Tipo,
            Titulo: request.Titulo,
            Descripcion: request.Descripcion,
            Estado: EstadoTicket.Abierto,
            FechaCreacion: DateTime.UtcNow,
            FechaCierre: null
        );

        return CreatedAtAction(nameof(ObtenerTicketPorId), new { id = nuevoTicket.Id }, nuevoTicket);
    }

    /// <summary>
    /// Obtiene el detalle y estado de un ticket por su identificador
    /// </summary>
    [HttpGet("tickets/{id:guid}")]
    public ActionResult<TicketResponse> ObtenerTicketPorId(Guid id)
    {
        var mock = new TicketResponse(
            Id: id,
            UsuarioId: "usr-456",
            ViajeId: "v-123",
            Tipo: TipoIncidencia.Viaje,
            Titulo: "Conductor no llegó a horario",
            Descripcion: "El conductor canceló sin avisar luego de 15 minutos.",
            Estado: EstadoTicket.EnRevision,
            FechaCreacion: DateTime.UtcNow.AddHours(-2),
            FechaCierre: null
        );

        return Ok(mock);
    }
}
