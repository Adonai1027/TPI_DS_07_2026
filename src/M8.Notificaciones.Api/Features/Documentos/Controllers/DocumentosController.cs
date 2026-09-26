using Microsoft.AspNetCore.Mvc;
using M8.Notificaciones.Api.Features.Documentos.Dtos;

namespace M8.Notificaciones.Api.Features.Documentos.Controllers;

[ApiController]
[Route("api/[controller]")]
[Tags("Documentos y QR (Célula 2)")]
public class DocumentosController : ControllerBase
{
    private readonly ILogger<DocumentosController> _logger;

    public DocumentosController(ILogger<DocumentosController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Genera un código QR temporal y de un solo uso para verificar el inicio del viaje (RF-8.3)
    /// </summary>
    [HttpPost("qr/generar")]
    public ActionResult<QrResponse> GenerarQr([FromBody] GenerarQrRequest request)
    {
        _logger.LogInformation("Generando token QR para Viaje {ViajeId}", request.ViajeId);

        var respuesta = new QrResponse(
            ViajeId: request.ViajeId,
            TokenSeguro: Guid.NewGuid().ToString("N"),
            QrBase64: "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mNk+A8AAQUBAScY42YAAAAASUVORK5CYII=",
            ExpiraEn: DateTime.UtcNow.AddMinutes(request.ValidezMinutos)
        );

        return Ok(respuesta);
    }

    /// <summary>
    /// Descarga el comprobante PDF de un viaje finalizado (RF-8.4)
    /// </summary>
    [HttpGet("comprobantes/{viajeId}/pdf")]
    public IActionResult DescargarComprobantePdf(string viajeId)
    {
        _logger.LogInformation("Solicitada descarga de comprobante para viaje {ViajeId}", viajeId);

        // Retorno simulado mientras se implementa QuestPDF
        var fakePdfBytes = System.Text.Encoding.UTF8.GetBytes($"%PDF-1.4 Mock Comprobante Viaje: {viajeId}");
        return File(fakePdfBytes, "application/pdf", $"comprobante-{viajeId}.pdf");
    }

    /// <summary>
    /// Reenvía el comprobante por correo electrónico (RF-8.5)
    /// </summary>
    [HttpPost("comprobantes/{viajeId}/reenviar")]
    public IActionResult ReenviarComprobante(string viajeId, [FromBody] ReenviarComprobanteRequest request)
    {
        _logger.LogInformation("Reenviando comprobante del viaje {ViajeId} a {Email}", viajeId, request.EmailDestino);

        return Accepted(new { Mensaje = $"Comprobante del viaje {viajeId} encolado para reenvío a {request.EmailDestino}" });
    }
}
