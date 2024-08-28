using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaVenta.DAL.Repositorios;
using SistemaVenta.DAL.Repositorios.Contrato;
using SistemaVenta.DLL.Servicios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DLL.Servicios
{
    public class CotizacionService:ICotizacionService

    {
        private readonly ICotizacionRepository _cotizacionRepository;

        private readonly IGenericRepository<Cotizacion> _cotizacionRepositorio;

        private readonly IGenericRepository<Servicio> _servicioRepositorio;

        private readonly IGenericRepository<Prospecto> _prospectoRepositorio;

        private readonly IGenericRepository<CotizacionServicio> _cotizacionServicioRepositorio;

        private readonly IMapper _mapper;

        public CotizacionService(IGenericRepository<Cotizacion> cotizacionRepositorio, IGenericRepository<Servicio> servicioRepositorio, IGenericRepository<Prospecto> prospectoRepositorio, IGenericRepository<CotizacionServicio> cotizacionServicioRepositorio, IMapper mapper, ICotizacionRepository cotizacionRepository)
        {
            _cotizacionRepository = cotizacionRepository;
            _cotizacionRepositorio = cotizacionRepositorio;
            _servicioRepositorio = servicioRepositorio;
            _prospectoRepositorio = prospectoRepositorio;
            _mapper = mapper;
        }

        public async Task<CotizacionDTO> Registrar(CotizacionDTO modelo)
        {
            try
            {
                var cotizacionGenerada = await _cotizacionRepository.Registrar(_mapper.Map<Cotizacion>(modelo));
                if (cotizacionGenerada.IdCotizacion == 0)
                {
                    throw new TaskCanceledException("No se pudo crear");
                }
                return _mapper.Map<CotizacionDTO>(cotizacionGenerada);
            }
            catch
            {
                throw;
            }
        }


        /* public async Task<bool> EditarCotizacion(CotizacionDTO modelo)
         {
             try
             {
                 var cotizacionGenerada = await _cotizacionRepository.EditarCotizacion(_mapper.Map<Cotizacion>(modelo));
                 if (cotizacionGenerada==false)
                 {
                     throw new TaskCanceledException("No se pudo crear");
                 }
                 return true;
             }
             catch
             {
                 throw;
             }
         }*/

        public async Task<List<CotizacionDTO>> Lista()
        {
            try
            {
                var queryCotizacion = await _cotizacionRepositorio.Consultar();
                return _mapper.Map<List<CotizacionDTO>>(queryCotizacion).ToList();
            }
            catch
            {
                throw;
            }
        }
        public async Task<CotizacionDTO> Crear(CotizacionDTO modelo)
        {
            try
            {
                var cotizacionCreado = await _cotizacionRepositorio.Crear(_mapper.Map<Cotizacion>(modelo));
                if (cotizacionCreado.IdCotizacion == 0)
                {
                    throw new TaskCanceledException("No se pudo crear el cotizacion");
                }
                return _mapper.Map<CotizacionDTO>(cotizacionCreado);

            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Editar(CotizacionDTO modelo)
        {
            IQueryable<Servicio> query = await _servicioRepositorio.Consultar();
            var listaResultado = new List<Servicio>();
            var listaResultadoDTO = _mapper.Map<ServicioDTO>(listaResultado);
            try
            {
                var cotizacionModelo = _mapper.Map<Cotizacion>(modelo);
                var cotizacionEncontrado = await _cotizacionRepositorio.Obtener(u => u.IdCotizacion == cotizacionModelo.IdCotizacion);

                if (cotizacionEncontrado == null)
                {
                    throw new TaskCanceledException("No existe el COTIZACION");
                }
                cotizacionEncontrado.IdProspecto = cotizacionModelo.IdProspecto;
                cotizacionEncontrado.Pulgadas2 = cotizacionModelo.Pulgadas2;
                cotizacionEncontrado.TipoPago = cotizacionModelo.TipoPago;
                cotizacionEncontrado.Total = cotizacionModelo.Total;
                cotizacionEncontrado.TiempoFinancing = cotizacionModelo.TiempoFinancing;
                cotizacionEncontrado.Ahorra = cotizacionModelo.Ahorra;
                cotizacionEncontrado.ValorIntereses = cotizacionModelo.ValorIntereses;
                cotizacionEncontrado.CuotaMensual = cotizacionModelo.CuotaMensual;
                cotizacionEncontrado.PagoElectricidad = cotizacionModelo.PagoElectricidad;
                cotizacionEncontrado.PorcentajeIncremento = cotizacionModelo.PorcentajeIncremento;
                cotizacionEncontrado.MensualAprox = cotizacionModelo.MensualAprox;
                cotizacionEncontrado.ValorPagado = cotizacionModelo.ValorPagado;
                cotizacionEncontrado.ProyeccionSolar = cotizacionModelo.ProyeccionSolar;
                cotizacionEncontrado.TiempoSolar = cotizacionModelo.TiempoSolar;
                cotizacionEncontrado.Contrato = cotizacionModelo.Contrato;
                cotizacionEncontrado.Email = cotizacionModelo.Email;
                cotizacionEncontrado.TipoIdentificacion = cotizacionModelo.TipoIdentificacion;
                cotizacionEncontrado.Identificacion = cotizacionModelo.Identificacion;
                cotizacionEncontrado.SocEin = cotizacionModelo.SocEin;
                cotizacionEncontrado.TamanoSistema = cotizacionModelo.TamanoSistema;
                cotizacionEncontrado.PagoInicial = cotizacionModelo.PagoInicial;
                cotizacionEncontrado.Notas = cotizacionModelo.Notas;
                cotizacionEncontrado.Url1 = cotizacionModelo.Url1;
                cotizacionEncontrado.Url2 = cotizacionModelo.Url2;
                cotizacionEncontrado.Url3 = cotizacionModelo.Url3;
                cotizacionEncontrado.Url4 = cotizacionModelo.Url4;
                cotizacionEncontrado.Pdf = cotizacionModelo.Pdf;


                bool respuesta = await _cotizacionRepositorio.Editar(cotizacionEncontrado);
                if (respuesta == false)
                {
                    throw new TaskCanceledException("No se pudo editar");
                }
                return respuesta;
            }
            catch
            {
                throw;
            }
        }

        public async  Task<bool> Eliminar(int id)
        {
            try
            {
                var cotizacionEncontrado = await _cotizacionRepositorio.Obtener(u => u.IdCotizacion == id);
                //donde usuario encontrado en la segunda parte no lleva await 

                if (cotizacionEncontrado == null)
                {
                    throw new TaskCanceledException("El cotizacion no existe");
                }

                bool respuesta = await _cotizacionRepositorio.Eliminar(cotizacionEncontrado);

                if (respuesta == false)
                {
                    throw new TaskCanceledException("No se pudo eliminar");

                }
                return respuesta;

            }
            catch
            {
                throw;
            }
        }

        
    }
}
