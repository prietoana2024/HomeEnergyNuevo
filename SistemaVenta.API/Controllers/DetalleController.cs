using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVenta.API.Utilidad;
using SistemaVenta.DLL.Servicios.Contrato;
using SistemaVenta.DTO;

namespace SistemaVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleController : ControllerBase
    {
        private readonly IDetalleVentaService _detalleVentaServicio;

        public DetalleController(IDetalleVentaService detalleVentaServicio)
        {
            _detalleVentaServicio = detalleVentaServicio;
        }

        [HttpGet]
        [Route("Lista")]

        public async Task<IActionResult> Lista()
        {
            var rsp = new Response<List<DetalleVentaDTO>>();

            try
            {
                rsp.Status = true;
                rsp.Value = await _detalleVentaServicio.Lista();
            }

            catch (Exception ex)
            {
                rsp.Msg = ex.Message;
            }
            //TODAS LOS SOLICITUDES SERÁN RESPUESTAS EXITOSAS
            return Ok(rsp);
        }

    }
}
