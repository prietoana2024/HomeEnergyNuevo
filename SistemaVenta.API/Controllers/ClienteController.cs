using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVenta.DLL.Servicios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.Models;
using SistemaVenta.API.Utilidad;
using AutoMapper;

namespace SistemaVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteServicio;

        public ClienteController(IClienteService clienteServicio)
        {
            _clienteServicio = clienteServicio;
        }
        [HttpPost]
        [Route("Registrar")]
        public async Task<IActionResult> Registrar([FromBody] ClienteDTO cliente)
        {
            var rsp = new Response<ClienteDTO>();
            try
            {
                rsp.Status = true;
                rsp.Value = await _clienteServicio.Registrar(cliente);
            }

            catch (Exception ex)
            {
                rsp.Status = false;
                rsp.Msg = ex.Message;
            }
            //TODAS LOS SOLICITUDES SERÁN RESPUESTAS EXITOSAS
            return Ok(rsp);
        }

        [HttpPost]
        [Route("RegistrarCliente")]
        public async Task<IActionResult> RegistrarCliente([FromBody] ClienteDTO cliente)
        {

            Console.WriteLine("Ha terminado la tarea");
            
            var rsp = new Response<ClienteDTO>();

            try
            {
                rsp.Status = true;
                rsp.Value = await _clienteServicio.RegistrarCliente(cliente);
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
