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
    public class DetalleVentaService:IDetalleVentaService
    {
        private readonly IGenericRepository<Venta> _ventaRepositorio;
        private readonly IGenericRepository<DetalleVenta> _detalleVentaRepositorio;
        private readonly IMapper _mapper;

        public DetalleVentaService(IGenericRepository<Venta> ventaRepositorio, IGenericRepository<DetalleVenta> detalleVentaRepositorio, IMapper mapper)
        {
            _ventaRepositorio = ventaRepositorio;
            _detalleVentaRepositorio = detalleVentaRepositorio;
            _mapper = mapper;
        }

        public async  Task<DetalleVentaDTO> Crear(DetalleVentaDTO modelo)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Editar(DetalleVentaDTO modelo)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<DetalleVentaDTO>> Lista()
        {
            try
            {
                var queryDetalle = await _detalleVentaRepositorio.Consultar();
                return _mapper.Map<List<DetalleVentaDTO>>(queryDetalle).ToList();
            }
            catch
            {
                throw;
            }
        }
    }
}
