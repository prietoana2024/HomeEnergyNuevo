using Microsoft.Data.SqlClient;
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

             var cn = new SqlConnection();

                //CREAMOS UNA VARIABLE
                Cliente clienteGenerado = new();

            Usuario usuario = new Usuario();

            Cliente clienteBuscar = new Cliente();

            //1-TRANSFORMAR EL DTO DE COTIZACION

           /* string connstring = "connection string";
            using (SqlConnection cn = new SqlConnection(connstring))
            {
                cn.Open();

                string query = @"INSERT INTO Cliente (nombre,fachadaimg,url,direccion,contacto,razonSocial,idauditor,detalle,esActivo,fecha,idProspecto)
                        VALUES(@nombre, @fachadaimg, @url, @direccion, @contacto,@razonSocial,@idauditor,@detalle,@esActivo,@fecha,@idProspecto)";

                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@nombre", modelo.Nombre);
                cmd.Parameters.AddWithValue("@fachadaimg", modelo.Fachadaimg);
                cmd.Parameters.AddWithValue("@url", modelo.Url);
                cmd.Parameters.AddWithValue("@direccion", modelo.Direccion);
                cmd.Parameters.AddWithValue("@contacto", modelo.Nombre);
                cmd.Parameters.AddWithValue("@razonSocial", modelo.RazonSocial);
                cmd.Parameters.AddWithValue("@idauditor", modelo.Idauditor);
                cmd.Parameters.AddWithValue("@detalle", modelo.Detalle);
                cmd.Parameters.AddWithValue("@esActivo", modelo.EsActivo);
                cmd.Parameters.AddWithValue("@fecha", modelo.Fecha);
                cmd.Parameters.AddWithValue("@idProspecto", modelo.IdProspecto);

                cmd.ExecuteNonQuery();
            }*/
            //si dentro de la logica ocurre un error la linea siguiente tiene que reestablecer todo al principio
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                   /* foreach (ClienteUsuario dv in modelo.ClienteUsuarios)
                    {
                        Usuario usuario_encontrado = _dbContext.Usuarios.Where(p => p.IdUsuario == dv.IdUsuario).First();

                        _dbContext.Usuarios.Update(usuario_encontrado);
                    }
                    await _dbContext.SaveChangesAsync();*/

                    //0001
                    Prospecto prospecto_encontrado = _dbContext.Prospectos.Where(p => p.IdProspecto == modelo.IdProspecto).First();
                    modelo.IdProspectoNavigation = prospecto_encontrado;
                     
                    modelo.FechaRegistro = DateTime.Now;

                    //var LastRegister = _dbContext.Clientes.OrderByDescending(x => x.IdCliente).First().IdCliente;

                    //  var ids = LastRegister+2;

                    //modelo.IdCliente = ids;
                    foreach (ClienteUsuario dv in modelo.ClienteUsuarios)
                    {

                        Usuario usuario_encontrado = _dbContext.Usuarios.Where(p => p.IdUsuario == dv.IdUsuario).First();

                        _dbContext.Usuarios.Update(usuario_encontrado);
                    }

                    await _dbContext.SaveChangesAsync();

                    //  await _dbContext.AddAsync(modelo);
                    // await _dbContext.SaveChangesAsync();


                    //la transaccion puede finalizar sin nigun problema
                    //  transaction.Commit();

                    //var numeroVenta1 = Convert.ToInt32(numeroVenta);
                    var idProspecto = Convert.ToInt32(modelo.IdProspecto);

                    modelo.IdProspectoNavigation = prospecto_encontrado;
                    if(modelo.Idauditor==0)
                    {
                        modelo.Idauditor = 1;
                    }

                    modelo.FechaRegistro = DateTime.Now;

                    modelo.EsActivo=true;

                    
                    await _dbContext.AddAsync(modelo);
                    await _dbContext.SaveChangesAsync();

                    // cotizacionGenerada = modelo;
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
                return modelo;
            }
            

        }
        public async Task<bool> EditarCliente(Cliente modelo)
        {
            //CREAMOS UNA VARIABLE
            Cliente clienteGenerada = new();

            Prospecto prospecto = new Prospecto();

            //1-TRANSFORMAR EL DTO DE COTIZACION


            //si dentro de la logica ocurre un error la linea siguiente tiene que reestablecer todo al principio
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {


                    var clienteModelo = modelo;

                    var clienteEncontrado = _dbContext.Clientes.Where(p => p.IdCliente == modelo.IdCliente).First();

                    var clientesAnteriores = _dbContext.ClienteUsuarios.Where(p => p.IdCliente == modelo.IdCliente).First();



                    foreach (ClienteUsuario di in modelo.ClienteUsuarios)
                    {
                        Usuario usuario_encontrado = _dbContext.Usuarios.Where(p => p.IdUsuario == di.IdUsuario).First();

                        _dbContext.Usuarios.Update(usuario_encontrado);
                    }
                    await _dbContext.SaveChangesAsync();

                    //var clienteEncontrado = await _cotizacionRepositorio.Obtener(u => u.IdCotizacion == clienteModelo.IdCotizacion);

                    if (clienteEncontrado == null)
                    {
                        throw new TaskCanceledException("No existe el COTIZACION");
                    }
                    clienteEncontrado.Nombre = clienteModelo.Nombre;
                    clienteEncontrado.EsActivo = clienteModelo.EsActivo;
                    clienteEncontrado.Fecha = clienteModelo.Fecha;
                    clienteEncontrado.FechaRegistro = clienteModelo.FechaRegistro;
                    clienteEncontrado.Fachadaimg = clienteModelo.Fachadaimg;
                    clienteEncontrado.Url = clienteModelo.Url;
                    clienteEncontrado.Idauditor = clienteModelo.Idauditor;
                    clienteEncontrado.RazonSocial = clienteModelo.RazonSocial;
                    clienteEncontrado.Contacto = clienteModelo.Contacto;
                    clienteEncontrado.Direccion = clienteModelo.Direccion;
                    clienteEncontrado.Detalle = clienteModelo.Detalle;
                    clienteEncontrado.IdProspecto = clienteModelo.IdProspecto;


                    //se cayo aqui- probar si fue por l abd esta sola

                    var idProspecto = Convert.ToInt32(modelo.IdProspecto);

                    Prospecto prospecto_encontrado = _dbContext.Prospectos.Where(p => p.IdProspecto == modelo.IdProspecto).First();

                    modelo.IdProspectoNavigation = prospecto_encontrado;

                    clienteEncontrado.FechaRegistro = DateTime.Now;

                    _dbContext.Set<Cliente>().Update(clienteEncontrado);
                    await _dbContext.SaveChangesAsync();

                    /*await _dbContext.AddAsync(modelo);
                    await _dbContext.SaveChangesAsync();
                    */
                    clienteGenerada = modelo;
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
                return true;
            }
        }

        
    }
}
