using AutoMapper;
using SistemaVenta.DAL.Repositorios;
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
    public class ClientesService2 : IClientesService2
    {
        private readonly IClienteRepository _clientes2Repository;

        private readonly IGenericRepository<Cliente> _clientes2Repositorio;

        private readonly IMapper _mapper;

        public ClientesService2(IClienteRepository clientes2Repository, IGenericRepository<Cliente> clientes2Repositorio, IMapper mapper)
        {
            _clientes2Repository = clientes2Repository;
            _clientes2Repositorio = clientes2Repositorio;
            _mapper = mapper;
        }

        public async  Task<ClienteDTO> Registrar(ClienteDTO modelo)
        {
           /* var model = new Cliente();
            model.Contacto = modelo.Contacto;
            model.Detalle = modelo.Detalle;
            model.Direccion = modelo.Direccion;
            model.EsActivo = modelo.EsActivo;
            model.Fachadaimg = modelo.Fachadaimg;
            model.Fecha = modelo.Fecha;
            model.FechaRegistro = modelo.FechaRegistro;
            model.IdCliente = modelo.IdCliente;
            model.IdProspecto = modelo.IdProspecto;
            model.IdProspectoNavigation = modelo.IdProspecto;
            model.Idauditor = modelo.Idauditor;
            model.Nombre = modelo.Nombre;
            model.RazonSocial = modelo.RazonSocial;
            model.Url = modelo.Url;
            model.ClienteUsuarios = modelo.Usuarios;*/

            try
            {
                var ventaGenerada = await _clientes2Repository.Registrar(_mapper.Map<Cliente>(modelo));
                if (ventaGenerada.IdCliente == 0)
                {
                    throw new TaskCanceledException("No se pudo crear");
                }
                return _mapper.Map<ClienteDTO>(ventaGenerada);
            }
            catch
            {
                throw;
            }
        }

    }
}
