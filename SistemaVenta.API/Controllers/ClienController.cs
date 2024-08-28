using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVenta.API.Utilidad;
using SistemaVenta.DLL.Servicios.Contrato;
using SistemaVenta.DTO;

namespace SistemaVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienController : ControllerBase
    {
        private readonly IClientesService2 _clientes2Servicio;

        public ClienController(IClientesService2 clientes2Servicio)
        {
            _clientes2Servicio = clientes2Servicio;
        }

        [HttpPost]
        [Route("Registrar")]
        public async Task<IActionResult> Registrar([FromBody] ClienteDTO cliente)
        {
            var rsp = new Response<ClienteDTO>();
            try
            {
                rsp.Status = true;
                rsp.Value = await _clientes2Servicio.Registrar(cliente);
            }

            catch (Exception ex)
            {
                rsp.Status = false;
                rsp.Msg = ex.Message;
            }
            //TODAS LOS SOLICITUDES SERÁN RESPUESTAS EXITOSAS
            return Ok(rsp);
        }
    }
}
