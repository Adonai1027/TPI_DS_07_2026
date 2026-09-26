namespace M8.Notificaciones.Api.Features.Documentos.Entities;

public class Comprobante
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string ViajeId { get; set; } = string.Empty;
    public string ClienteId { get; set; } = string.Empty;
    public decimal MontoTotal { get; set; }
    public string Moneda { get; set; } = "ARS";
    public DateTime FechaEmision { get; set; } = DateTime.UtcNow;
    public string? RutaArchivoPdf { get; set; }
    public bool EnviadoPorEmail { get; set; } = false;
}
