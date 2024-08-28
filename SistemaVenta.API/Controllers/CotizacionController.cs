using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SistemaVenta.API.Utilidad;
using SistemaVenta.DLL.Servicios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.Models;

namespace SistemaVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CotizacionController : ControllerBase
    {
        private readonly ICotizacionService _cotizacionServicio;

        public CotizacionController(ICotizacionService cotizacionServicio)
        {
            _cotizacionServicio = cotizacionServicio;
        }

        [HttpGet]
        [Route("Lista")]

        public async Task<IActionResult> Lista()
        {
            var rsp = new Response<List<CotizacionDTO>>();

            try
            {
                rsp.Status = true;
                rsp.Value = await _cotizacionServicio.Lista();
            }

            catch (Exception ex)
            {
                rsp.Msg = ex.Message;
            }
            //TODAS LOS SOLICITUDES SERÁN RESPUESTAS EXITOSAS
            return Ok(rsp);
        }

       
        /*
                [HttpGet]
                [Route("ListaServicios")]

                public async Task<IActionResult> ListaServicios()
                {
                    var rsp = new Response<List<CotizacionDTO>>();

                    try
                    {
                        rsp.Status = true;
                        //rsp.Value = await _cotizacionServicio.Registrar(cotizacion);
                        rsp.Value = await _cotizacionServicio.ListaServicios();
                    }

                    catch (Exception ex)
                    {
                        rsp.Msg = ex.Message;
                    }
                    //TODAS LOS SOLICITUDES SERÁN RESPUESTAS EXITOSAS
                    return Ok(rsp);
                }
        */
        [HttpPost]
        [Route("Guardar")]


        public async Task<IActionResult> Guardar([FromBody] CotizacionDTO cotizacion)
        {
            var rsp = new Response<CotizacionDTO>();

            try
            {
                rsp.Status = true;
                rsp.Value = await _cotizacionServicio.Crear(cotizacion);
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
        public async Task<IActionResult> Editar([FromBody] CotizacionDTO cotizacion)
        {
            var rsp = new Response<bool>();

            try
            {
                rsp.Status = true;
                rsp.Value = await _cotizacionServicio.Editar(cotizacion);
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
                rsp.Value = await _cotizacionServicio.Eliminar(id);
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
        [Route("Registrar")]
        public async Task<IActionResult> Registrar([FromBody] CotizacionDTO cotizacion)
        {
            var rsp = new Response<CotizacionDTO>();
            try
            {
                rsp.Status = true;
                rsp.Value = await _cotizacionServicio.Registrar(cotizacion);
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
        [Route("AgregarCotizacion")]
        public async Task<IActionResult> Agregar([FromForm] CotizacionDTO cotizacion)
        {

            var rsp = new Response<CotizacionDTO>();

            try
            {
                rsp.Status = true;
                rsp.Value = await _cotizacionServicio.Crear(cotizacion);
            }

            catch (Exception ex)
            {
                rsp.Status = false;
                rsp.Msg = ex.Message;
            }
            //TODAS LOS SOLICITUDES SERÁN RESPUESTAS EXITOSAS
            return Ok(rsp);

            // var imagenModelo = _mapper.Map<Imagen>(imagen);
            // var fileModelo =new FileData();
           /* var rsp = new Response<CotizacionDTO>();
            var servicios = new ServicioDTO();
            ServicioDTO service = new ServicioDTO();

            var result = await _cotizacionServicio.Crear(cotizacion); 

            return Ok(rsp.Msg = "COPIA EL NOMBRE DE TU IMAGEN");*/


        }
    }
}
