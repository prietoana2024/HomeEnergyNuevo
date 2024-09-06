using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto;
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

        public async Task<List<ClienteDTO>> Lista(int idUsuario)
        {
            
           /* IQueryable<MenuRol> tbMenuRol = await _menuRolRepositorio.Consultar();
            IQueryable<Usuario> tbUsuario = await _usuarioRepositorio.Consultar();
            IQueryable<Menu> tbMenu = await _menuRepositorio.Consultar();*/

           /* IQueryable<ClienteUsuario> tbClienteUsuario = await _clienteUsuarioRepositorio.Consultar();
            IQueryable<Usuario> tbUsuario = await _usuarioRepositorio.Consultar();*/
            IQueryable<Cliente> query = await _clienteRepositorio.Consultar(u => u.Idauditor == idUsuario);

            try
            {
                var listaClientes = query.ToList();
                return _mapper.Map<List<ClienteDTO>>(listaClientes);


                
            }
            catch
            {
                throw;
            }/*

            try
            {
                var queryclienteUsuarioCreado = await _clienteUsuarioRepositorio.Consultar();
                return _mapper.Map<List<ClienteUsuarioDTO>>(queryclienteUsuarioCreado).ToList();
            }
            catch
            {
                throw;
            }*/
        }/*
        public async  Task<List<ClientesXUsuarioDTO>> ListaConUsuarios()
        {
            IQueryable<ClienteUsuario> tbClienteUsuario = await _clienteUsuarioRepositorio.Consultar();
            IQueryable<Usuario> tbUsuario = await _usuarioRepositorio.Consultar();
            IQueryable<Cliente> tbCliente = await _clienteRepositorio.Consultar();


            ClientesXUsuarioDTO clienteData = new();
            try
            {
                IQueryable tbResultado = (from u in tbCliente
                                                   join mr in tbClienteUsuario on u.IdCliente equals mr.IdCliente
                                                   join m in tbUsuario on mr.IdUsuario equals  m.IdUsuario
                                          select u).AsQueryable();

                IQueryable tbResultado2= (from u in tbUsuario
                                          join mr in tbClienteUsuario on u.IdUsuario equals mr.IdUsuario
                                          join m in tbCliente on mr.IdCliente equals m.IdCliente
                                          select u).AsQueryable();

                var listaClientes = tbResultado;


                return _mapper.Map<List<ClientesXUsuarioDTO>>(listaClientes);


            }
            catch
            {
                throw;
            }
        }*/


        public async Task<List<ClienteAsignadoDTO>> UsuariosXClientesListaNombre(string nombreUsuario)
        {
            IQueryable<ClienteUsuario> query = await _clienteUsuarioRepositorio.Consultar();
            /*  IQueryable<Usuario> tbUsuario = await _usuarioRepositorio.Consultar();
              IQueryable<Cliente> tbCliente = await _clienteRepositorio.Consultar();*/
            // IQueryable<DetalleVenta> query = await _detalleVentaRepositorio.Consultar();
            //ESE RETORNA UNA LISTA DEL TIPO DETALLE VENTA
            var listaResultado = new List<ClienteUsuario>();

            var listaFInal = new List<ClienteUsuario>();
            try
            {
                listaResultado = await query.Include(p => p.IdUsuarioNavigation).Include(v => v.IdClienteNavigation).ToListAsync();

                foreach (ClienteUsuario dv in listaResultado)
                {
                    ClienteUsuario cliente_encontrado = listaResultado.Where(p => p.IdUsuarioNavigation.NombreCompleto == nombreUsuario).First();
                     if(dv.IdUsuarioNavigation.NombreCompleto== nombreUsuario)
                    {
                        listaFInal.Add(cliente_encontrado);
                    }
                    
                }
                   
            }
            catch
            {
                throw;
            }
            return _mapper.Map<List<ClienteAsignadoDTO>>(listaFInal);
        }
        public async Task<List<ClienteAsignadoDTO>> UsuariosXClientesLista()
        {
            IQueryable<ClienteUsuario> query = await _clienteUsuarioRepositorio.Consultar();
          /*  IQueryable<Usuario> tbUsuario = await _usuarioRepositorio.Consultar();
            IQueryable<Cliente> tbCliente = await _clienteRepositorio.Consultar();*/
           // IQueryable<DetalleVenta> query = await _detalleVentaRepositorio.Consultar();
            //ESE RETORNA UNA LISTA DEL TIPO DETALLE VENTA
            var listaResultado = new List<ClienteUsuario>();
            try
            {
                listaResultado = await query.Include(p => p.IdUsuarioNavigation).Include(v => v.IdClienteNavigation).ToListAsync();
            }
            catch
            {
                throw;
            }
            return _mapper.Map<List<ClienteAsignadoDTO>>(listaResultado);
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
