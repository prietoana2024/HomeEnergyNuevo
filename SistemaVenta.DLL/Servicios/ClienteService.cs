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
        private readonly IClienteRepository _clienteRepository;

        private readonly IGenericRepository<Cliente> _clienteModeloRepositorio;

        private readonly IMapper _mapper;

        public ClienteService(IClienteRepository clienteRepository, IGenericRepository<Cliente> clienteModeloRepositorio, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _clienteModeloRepositorio = clienteModeloRepositorio;
            _mapper = mapper;
        }

        public async  Task<ClienteDTO> Registrar(ClienteDTO modelo)
        {
            try
            {
                var clienteGenerada = await _clienteRepository.Registrar(_mapper.Map<Cliente>(modelo));
                if (clienteGenerada.IdCliente == 0)
                {
                    throw new TaskCanceledException("No se pudo crear");
                }
                return _mapper.Map<ClienteDTO>(clienteGenerada);
            }
            catch
            {
                throw;
            }
        }

        public async Task<ClienteDTO> RegistrarCliente(ClienteDTO modelo)
        {
            var task = new Task(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("tarea terminada");
            });
            task.Start();

            await task;

            try
            {
               /* _dbcontext.Clientes.Add(modelo);
                //guardar cambios de manera asincrona asi:
                await _dbcontext.SaveChangesAsync();
                return modelo;*/
                var clienteGenerada = await _clienteRepository.RegistrarCliente(_mapper.Map<Cliente>(modelo));
                if (clienteGenerada.IdCliente == 0)
                {
                    throw new TaskCanceledException("No se pudo crear");
                }
                return _mapper.Map<ClienteDTO>(clienteGenerada);
            }
            catch
            {
                throw;
            }  
           
        }

    }
}
