using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    public class ClienteService : IClienteService
    {

        private readonly IGenericRepository<Cliente> _clienteRepositorio;

        private readonly IMapper _mapper;

        public ClienteService(IGenericRepository<Cliente> clienteRepositorio, IMapper mapper)
        {
            _clienteRepositorio = clienteRepositorio;
            _mapper = mapper;
        }

        public async Task<List<ClienteDTO>> Lista()
        {
            try
            {
                var queryProducto = await _clienteRepositorio.Consultar();
                return _mapper.Map<List<ClienteDTO>>(queryProducto).ToList();
            }
            catch
            {
                throw;
            }
        }
        public async Task<ClienteDTO> Crear(ClienteDTO modelo)
        {
            try
            {
                var clienteCreado = await _clienteRepositorio.Crear(_mapper.Map<Cliente>(modelo));
                if (clienteCreado.IdCliente == 0)
                {
                    throw new TaskCanceledException("No se pudo crear el cliente");
                }
                return _mapper.Map<ClienteDTO>(clienteCreado);

            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Editar(ClienteDTO modelo)
        {


            try
            {
                var clienteModelo = _mapper.Map <Cliente>(modelo);
                var clienteCreado = await _clienteRepositorio.Editar(_mapper.Map<Cliente>(modelo));
                var clienteEncontrado = await _clienteRepositorio.Obtener(u => u.IdCliente == clienteModelo.IdCliente);
                if (clienteEncontrado == null)
                {
                    throw new TaskCanceledException("cliente no existe");
                }


                clienteEncontrado.Nombre = clienteModelo.Nombre;
                clienteEncontrado.Fachadaimg = clienteModelo.Fachadaimg;
                clienteEncontrado.Idauditor = clienteModelo.Idauditor;
                clienteEncontrado.Url = clienteModelo.Url;
                clienteEncontrado.Direccion = clienteModelo.Direccion;
                clienteEncontrado.Contacto = clienteModelo.Contacto;
                clienteEncontrado.RazonSocial = clienteModelo.RazonSocial;
                clienteEncontrado.EsActivo = clienteModelo.EsActivo;


                bool respuesta = await _clienteRepositorio.Editar(clienteEncontrado);

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

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var clienteEncontrado = await _clienteRepositorio.Obtener(u => u.IdCliente == id);
                //donde usuario encontrado en la segunda parte no lleva await 

                if (clienteEncontrado == null)
                {
                    throw new TaskCanceledException("El cliente no existe");
                }

                bool respuesta = await _clienteRepositorio.Eliminar(clienteEncontrado);

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
