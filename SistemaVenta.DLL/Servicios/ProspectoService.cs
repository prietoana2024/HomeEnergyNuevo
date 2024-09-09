using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class ProspectoService:IProspectoService
    {
        private readonly IGenericRepository<Cliente> _clienteRepositorio;

        private readonly IGenericRepository<Prospecto> _prospectoRepositorio;

        private readonly IMapper _mapper;

        public ProspectoService(IGenericRepository<Cliente> clienteRepositorio, IGenericRepository<Prospecto> prospectoRepositorio, IMapper mapper)
        {
            _clienteRepositorio = clienteRepositorio;
            _mapper = mapper;
            _prospectoRepositorio = prospectoRepositorio;
        }
        public async Task<List<ProspectoDTO>> Lista()
        {
            try
            {
                var queryProducto = await _prospectoRepositorio.Consultar();
                return _mapper.Map<List<ProspectoDTO>>(queryProducto).ToList();
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<ProspectoDTO>> ListaNoConvertidos()
        {
            /*  IQueryable<Cliente> tbCliente = await _clienteRepositorio.Consultar(u => u.IdProspecto == idUsuario);
              IQueryable<MenuRol> tbMenuRol = await _menuRolRepositorio.Consultar();
              try
              {
                  var queryProducto = await _prospectoRepositorio.Consultar();
                  return _mapper.Map<List<ProspectoDTO>>(queryProducto).ToList();
              }
              catch
              {
                  throw;
              }*/
            IQueryable<Cliente> tbCliente = await _clienteRepositorio.Consultar();
            IQueryable<Prospecto> tbProspecto = await _prospectoRepositorio.Consultar();

            var lista=new List<Prospecto>();
            /*
             foreach (var sucursal in lstSucursal)
            {
                foreach (var central in lstCentral)
                {
                    if (sucursal.codigo != central.codigo )
                    {
                        if (sucursal.presinto != central.presinto )
                        {
                            lstInsert.Add(sucursal);
                        }
                    }
                   break;
                }
            }*/
            try
            {
                /*var lstInsert = from a in lstSucursal
                from b in lstCentral
                where a.codigo != b.codigo && a.presinto != b.presinto
                select a;*/
                /* IQueryable<Prospecto> tbResultado = (from a in tbProspecto
                                 from b in tbCliente
                                 where a.IdProspecto != b.IdProspecto
                                 select a).AsQueryable();*/
               /* var lstInsert = from a in tbProspecto
                                join b in tbCliente on a.Nombre equals b.Nombre
                                select a;

                foreach (Prospecto dv in tbProspecto)
                {
                    var milista = lstInsert.Where(p => p.IdProspecto != dv.IdProspecto).First();

                    lista.Add(milista);
                }*/
               /*foreach (var item in tbProspecto)
                {

                    foreach (var item2 in lstInsert)
                    {
                        if (item.IdProspecto != item2.IdProspecto)
                        {
                            lista.Add(item);
                        }
                   
                }}*/
                var listaProspectos = lista.ToList();
                return _mapper.Map<List<ProspectoDTO>>(listaProspectos);
            }
            catch
            {
                throw;
            }


        }


        public async Task<ProspectoDTO> Crear(ProspectoDTO modelo)
        {
            try
            {
                var prospectoCreado = await _prospectoRepositorio.Crear(_mapper.Map<Prospecto>(modelo));
                if (prospectoCreado.IdProspecto == 0)
                {
                    throw new TaskCanceledException("No se pudo crear el Prospecto");
                }
                return _mapper.Map<ProspectoDTO>(prospectoCreado);

            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Editar(ProspectoDTO modelo)
        {


            try
            {
                var prospectoModelo = _mapper.Map<Prospecto>(modelo);
                var prospectoCreado = await _prospectoRepositorio.Editar(_mapper.Map<Prospecto>(modelo));
                var prospectoEncontrado = await _prospectoRepositorio.Obtener(u => u.IdProspecto == prospectoModelo.IdProspecto);
                if (prospectoEncontrado == null)
                {
                    throw new TaskCanceledException("Prospecto no existe");
                }

                prospectoEncontrado.Nombre = prospectoModelo.Nombre;
                prospectoEncontrado.Fachadaimg = prospectoModelo.Fachadaimg;
                prospectoEncontrado.Idauditor = prospectoModelo.Idauditor;
                prospectoEncontrado.Url = prospectoModelo.Url;
                prospectoEncontrado.Direccion = prospectoModelo.Direccion;
                prospectoEncontrado.Contacto = prospectoModelo.Contacto;
                prospectoEncontrado.RazonSocial = prospectoModelo.RazonSocial;
                prospectoEncontrado.EsActivo = prospectoModelo.EsActivo;


                bool respuesta = await _prospectoRepositorio.Editar(prospectoEncontrado);

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
        /* 
        public async Task<bool> Desactivar(int idProspecto)
        {


            try
            {
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
                return respuesta;
            }
            catch
            {
                throw;
            }
        }*/

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var prospectoEncontrado = await _prospectoRepositorio.Obtener(u => u.IdProspecto == id);
                //donde usuario encontrado en la segunda parte no lleva await 

                if (prospectoEncontrado == null)
                {
                    throw new TaskCanceledException("El prospecto no existe");
                }

                bool respuesta = await _prospectoRepositorio.Eliminar(prospectoEncontrado);

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
