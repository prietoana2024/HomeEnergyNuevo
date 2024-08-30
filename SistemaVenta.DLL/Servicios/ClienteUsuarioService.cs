using AutoMapper;
using Org.BouncyCastle.Crypto;
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
    public class ClienteUsuarioService:IClienteUsuarioService
    {

        private readonly IGenericRepository<ClienteUsuario> _clienteUsuarioRepositorio;

        private readonly IGenericRepository<Cliente> _clienteRepositorio;

        private readonly IGenericRepository<Usuario> _usuarioRepositorio;


        private readonly IMapper _mapper;

        public ClienteUsuarioService(IGenericRepository<ClienteUsuario> clienteUsuarioRepositorio, IGenericRepository<Cliente> clienteRepositorio, IGenericRepository<Usuario> usuarioRepositorio, IMapper mapper)
        {
            _clienteUsuarioRepositorio = clienteUsuarioRepositorio;
            _clienteRepositorio = clienteRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
            _mapper = mapper;
        }

        public async Task<List<ClienteUsuarioDTO>> Lista()
        {
            try
            {
                var queryclienteUsuarioCreado = await _clienteUsuarioRepositorio.Consultar();
                return _mapper.Map<List<ClienteUsuarioDTO>>(queryclienteUsuarioCreado).ToList();
            }
            catch
            {
                throw;
            }
        }

        public async  Task<ClienteUsuarioDTO> Crear(ClienteUsuarioDTO modelo)
        {
            try
            {
                var clienteEncontrado = await _clienteRepositorio.Obtener(u => u.IdCliente == modelo.IdCliente);


                var usuarioEncontrado = await _usuarioRepositorio.Obtener(u => u.IdUsuario == modelo.IdUsuario);

                //EN EL FRONT DEBE RECORRER UN ARREGLO Y CADA VEZ QUE VENGA, ENTRARÁ ACÁ

                ClienteUsuario modeloConvertido = new();

                modeloConvertido = _mapper.Map<ClienteUsuario>(modelo);

                modeloConvertido.IdClienteNavigation = clienteEncontrado;
                modeloConvertido.IdUsuarioNavigation = usuarioEncontrado;

                var clienteUsuarioCreado = await _clienteUsuarioRepositorio.Crear(modeloConvertido);
                if (clienteUsuarioCreado.IdClienteUsuario == 0)
                {
                    throw new TaskCanceledException("No se pudo crear el cliente");
                }
                return _mapper.Map<ClienteUsuarioDTO>(clienteUsuarioCreado);

            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Editar(ClienteUsuarioDTO modelo)
        {

            try
            {
                var clienteModelo = _mapper.Map<ClienteUsuario>(modelo);
                var clienteCreado = await _clienteUsuarioRepositorio.Editar(_mapper.Map<ClienteUsuario>(modelo));
                var clienteEncontrado = await _clienteUsuarioRepositorio.Obtener(u => u.IdCliente == clienteModelo.IdCliente);
                if (clienteEncontrado == null)
                {
                    throw new TaskCanceledException("La asignacion del cliente  no  se puede realizar, porque no existe");
                }


                clienteEncontrado.IdUsuario = clienteModelo.IdUsuario;
                clienteEncontrado.IdCliente = clienteModelo.IdCliente;


                bool respuesta = await _clienteUsuarioRepositorio.Editar(clienteEncontrado);

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
                var clienteUsuarioEncontrado = await _clienteUsuarioRepositorio.Obtener(u => u.IdClienteUsuario == id);
                //donde usuario encontrado en la segunda parte no lleva await 

                if (clienteUsuarioEncontrado == null)
                {
                    throw new TaskCanceledException("La asignacion del cliente  no  se puede eliminar, porque no existe");
                }

                bool respuesta = await _clienteUsuarioRepositorio.Eliminar(clienteUsuarioEncontrado);

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
