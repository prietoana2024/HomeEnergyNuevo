using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVenta.API.Utilidad;
using SistemaVenta.DLL.Servicios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.Models;

namespace SistemaVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteUsuarioController : ControllerBase
    {
        private readonly IClienteUsuarioService _clienteUsuarioServicio;

        public ClienteUsuarioController(IClienteUsuarioService clienteUsuarioServicio)
        {
            _clienteUsuarioServicio = clienteUsuarioServicio;
        }

        [HttpGet]
        [Route("Reporte")]

        public async Task<IActionResult> Reporte()
        {
            var rsp = new Response<List<ClienteAsignadoDTO>>();
            try
            {
                rsp.Status = true;
                rsp.Value = await _clienteUsuarioServicio.UsuariosXClientesLista();
            }
            catch (Exception ex)
            {
                rsp.Status = false;
                rsp.Msg = ex.Message;
            }
            //TODAS LOS SOLICITUDES SERÁN RESPUESTAS EXITOSAS
            return Ok(rsp);
        }
        [HttpGet]
        [Route("ReporteUsuario")]

        public async Task<IActionResult> ReporteUsuario(string nombreUsuario)
        {
            var rsp = new Response<List<ClienteAsignadoDTO>>();
            try
            {
                rsp.Status = true;
                rsp.Value = await _clienteUsuarioServicio.UsuariosXClientesListaNombre(nombreUsuario);
            }
            catch (Exception ex)
            {
                rsp.Status = false;
                rsp.Msg = ex.Message;
            }
            //TODAS LOS SOLICITUDES SERÁN RESPUESTAS EXITOSAS
            return Ok(rsp);
        }

        [HttpGet]
        [Route("Lista")]

        public async Task<IActionResult> Lista(int idUsuario)
        {
            var rsp = new Response<List<ClienteDTO>>();
          //  var rsp = new Response<List<ClienteUsuarioDTO>>();
            try
            {
                rsp.Status = true;
                rsp.Value = await _clienteUsuarioServicio.Lista(idUsuario);
            }

            catch (Exception ex)
            {
                rsp.Msg = ex.Message;
            }
            //TODAS LOS SOLICITUDES SERÁN RESPUESTAS EXITOSAS
            return Ok(rsp);
        }
        [HttpPost]
        [Route("Guardar")]


        public async Task<IActionResult> Guardar([FromBody] ClienteUsuarioDTO clienteUsuario)
        {
            var rsp = new Response<ClienteUsuarioDTO>();

            try
            {
                rsp.Status = true;
                rsp.Value = await _clienteUsuarioServicio.Crear(clienteUsuario);
            }

            catch (Exception ex)
            {
                rsp.Status = false;
                rsp.Msg = ex.Message;
            }
            //TODAS LOS SOLICITUDES SERÁN RESPUESTAS EXITOSAS
            return Ok(rsp);
        }

        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] ClienteUsuarioDTO clienteUsuario)
        {
            var rsp = new Response<bool>();

            try
            {
                rsp.Status = true;
                rsp.Value = await _clienteUsuarioServicio.Editar(clienteUsuario);
            }

            catch (Exception ex)
            {
                rsp.Status = false;
                rsp.Msg = ex.Message;
            }
            //TODAS LOS SOLICITUDES SERÁN RESPUESTAS EXITOSAS
            return Ok(rsp);
        }
        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var rsp = new Response<bool>();

            try
            {
                rsp.Status = true;
                rsp.Value = await _clienteUsuarioServicio.Eliminar(id);
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
