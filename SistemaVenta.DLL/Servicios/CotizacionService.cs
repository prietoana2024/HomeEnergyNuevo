using AutoMapper;
using SistemaVenta.DAL.Repositorios.Contrato;
using SistemaVenta.DLL.Servicios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DLL.Servicios
{
    public class CotizacionService:ICotizacionService
    {
        private readonly IGenericRepository<Cotizacion> _cotizacionRepositorio;

        private readonly IGenericRepository<Servicio> _servicioRepositorio;

        private readonly IGenericRepository<Prospecto> _prospectoRepositorio;

        private readonly IMapper _mapper;

        public CotizacionService(IGenericRepository<Cotizacion> cotizacionRepositorio, IGenericRepository<Servicio> servicioRepositorio, IGenericRepository<Prospecto> prospectoRepositorio, IMapper mapper)
        {
            _cotizacionRepositorio = cotizacionRepositorio;
            _servicioRepositorio = servicioRepositorio;
            _prospectoRepositorio = prospectoRepositorio;
            _mapper = mapper;
        }
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
        public Task<CotizacionDTO> Crear(CotizacionDTO modelo)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Editar(CotizacionDTO modelo)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        
    }
}
