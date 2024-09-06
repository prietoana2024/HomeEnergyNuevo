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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SistemaVenta.DLL.Servicios
{
    public class ClienteService : IClienteService
    {

        private readonly IGenericRepository<Cliente> _clienteRepositorio;

        private readonly IMapper _mapper;


        private readonly IGenericRepository<Prospecto> _prospectoRepositorio;

        public ClienteService(IGenericRepository<Cliente> clienteRepositorio, IMapper mapper, IGenericRepository<Prospecto> prospectoRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
            _mapper = mapper;
            _prospectoRepositorio = prospectoRepositorio;
        }

        public async Task<List<ClienteDTO>> Lista()
        {
            var listaResultado = new List<Cliente>();
            try
            {
                var queryCliente = await _clienteRepositorio.Consultar();
                listaResultado = await queryCliente.Include(p => p.IdProspectoNavigation).ToListAsync();
               
                return _mapper.Map<List<ClienteDTO>>(listaResultado).ToList();
            }
            catch
            {
                throw;
            }
        }
        public async Task<ClienteDTO> Crear(ClienteDTO modelo)
        {
            var Fecha = DateTime.Now;
            modelo.Fecha= Fecha.ToString("dd/MM/yyyy");

            var FechaRegistro = DateTime.Now;
            modelo.FechaRegistro = FechaRegistro.ToString("dd/MM/yyyy");
            try
            {
                var clienteEncontrado = await _clienteRepositorio.Obtener(u => u.IdProspecto == modelo.IdProspecto);
                if(clienteEncontrado == null) 
                {
                    var clienteCreado = await _clienteRepositorio.Crear(_mapper.Map<Cliente>(modelo));
                    if (clienteCreado.IdCliente == 0)
                    {
                        throw new TaskCanceledException("No se pudo crear el cliente");
                    }
                    else
                    {
                        var idProspecto = modelo.IdProspecto;
                        var prospectoEncontrado = await _prospectoRepositorio.Obtener(u => u.IdProspecto == idProspecto);
                        if (prospectoEncontrado == null)
                        {
                            throw new TaskCanceledException("Prospecto no existe");
                        }

                        prospectoEncontrado.EsActivo = false;


                        bool respuesta = await _prospectoRepositorio.Editar(prospectoEncontrado);

                        if (respuesta == false)
                        {
                            throw new TaskCanceledException("No se pudo desactivar");
                        }
                    }

                    return _mapper.Map<ClienteDTO>(clienteCreado);
                }
                throw new TaskCanceledException("El cliente de este prospecto ya fue registrado");
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
