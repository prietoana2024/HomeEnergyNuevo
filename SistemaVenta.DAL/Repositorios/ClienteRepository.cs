using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SistemaVenta.DAL.DBContext;
using SistemaVenta.DAL.Repositorios.Contrato;
using SistemaVenta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DAL.Repositorios
{
    public class ClienteRepository : GenericRepository<Cliente>, IClienteRepository
    {
        private readonly DbhomeEnergyContext _dbContext;

        public ClienteRepository(DbhomeEnergyContext dbcontext) : base(dbcontext)
        {
            _dbContext = dbcontext;
        }
        public async Task<Cliente> Registrar(Cliente modelo)
        {
            //CREAMOS UNA VARIABLE
            Cliente cotizacionGenerada = new();

            Prospecto prospecto = new Prospecto();

            //1-TRANSFORMAR EL DTO DE COTIZACION


            //si dentro de la logica ocurre un error la linea siguiente tiene que reestablecer todo al principio
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (ClienteUsuario dv in modelo.ClienteUsuarios)
                    {
                        Usuario servicio_encontrado = _dbContext.Usuarios.Where(p => p.IdUsuario == dv.IdUsuario).First();

                        _dbContext.Usuarios.Update(servicio_encontrado);
                    }
                    await _dbContext.SaveChangesAsync();

                    Prospecto prospecto_encontrado = _dbContext.Prospectos.Where(p => p.IdProspecto == modelo.IdProspecto).First();

                    NumeroDocumento correlativo = _dbContext.NumeroDocumentos.First();
                    correlativo.UltimoNumero = correlativo.UltimoNumero + 1;
                    correlativo.FechaRegistro = DateTime.Now;
                    _dbContext.NumeroDocumentos.Update(correlativo);
                    await _dbContext.SaveChangesAsync();
                    //0001
                    int CantidadDigitos = 4;
                    string ceros = string.Concat(Enumerable.Repeat("0", CantidadDigitos));
                    string numeroVenta = ceros + correlativo.UltimoNumero.ToString();
                    numeroVenta = numeroVenta.Substring(numeroVenta.Length - CantidadDigitos, CantidadDigitos);


                    //se cayo aqui- probar si fue por l abd esta sola


                    var numeroVenta1 = Convert.ToInt32(numeroVenta);
                    var idProspecto = Convert.ToInt32(modelo.IdProspecto);

                    modelo.IdProspectoNavigation = prospecto_encontrado;

                    modelo.FechaRegistro = DateTime.Now;

                    await _dbContext.AddAsync(modelo);
                    await _dbContext.SaveChangesAsync();

                    cotizacionGenerada = modelo;
                    //la transaccion puede finalizar sin nigun problema
                    transaction.Commit();
                }
                catch
                {
                    //devolvera todo como estaba antes
                    transaction.Rollback();
                    //devuelve el error
                    throw;
                }
                return cotizacionGenerada;
            }
            //  throw new NotImplementedException();

        }

        public async Task<Cliente> RegistrarCliente(Cliente modelo)
        {
            Cliente clienteGenerado= new();

            Prospecto prospecto = new Prospecto();

            ClienteUsuario clienteUsuario = new() ;

            clienteGenerado.IdCliente = 0;

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    Prospecto prospecto_encontrado = _dbContext.Prospectos.Where(p => p.IdProspecto == modelo.IdProspecto).First();

                    var idProspecto = Convert.ToInt32(modelo.IdProspecto);

                    modelo.IdProspectoNavigation = prospecto_encontrado;

                    modelo.FechaRegistro = DateTime.Now;
                    clienteGenerado = modelo;
                    _dbContext.Clientes.Add(clienteGenerado);
                    await _dbContext.SaveChangesAsync();

                    clienteUsuario.IdCliente = clienteGenerado.IdCliente;
                    clienteUsuario.IdClienteNavigation=clienteGenerado;

                    

                    foreach (ClienteUsuario dv in modelo.ClienteUsuarios)
                    {
                        Usuario servicio_encontrado = _dbContext.Usuarios.Where(p => p.IdUsuario == dv.IdUsuario).First();

                        _dbContext.Usuarios.Update(servicio_encontrado);

                        clienteUsuario.IdUsuario = servicio_encontrado.IdUsuario;
                        clienteUsuario.IdUsuarioNavigation = servicio_encontrado;
                        _dbContext.ClienteUsuarios.Add(clienteUsuario);
                    }
                   
                    await _dbContext.SaveChangesAsync();

                  
                }
                catch
                {
                    //devolvera todo como estaba antes
                    transaction.Rollback();
                    //devuelve el error
                    throw;
                }
                return clienteGenerado;
            }
        }
    }


}
