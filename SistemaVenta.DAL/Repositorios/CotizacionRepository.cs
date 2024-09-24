using Microsoft.EntityFrameworkCore;
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
    public class CotizacionRepository : GenericRepository<Cotizacion>, ICotizacionRepository
    {

        private readonly DbhomeEnergyContext _dbContext;

        public CotizacionRepository(DbhomeEnergyContext dbcontext) : base(dbcontext)
        {
            _dbContext = dbcontext;
        }

        public async Task<Cotizacion> Registrar(Cotizacion modelo)
        {
            //CREAMOS UNA VARIABLE
            Cotizacion cotizacionGenerada = new();

            Prospecto prospecto = new Prospecto();

            //1-TRANSFORMAR EL DTO DE COTIZACION


            //si dentro de la logica ocurre un error la linea siguiente tiene que reestablecer todo al principio
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (CotizacionServicio dv in modelo.CotizacionServicios)
                    {
                        Servicio servicio_encontrado = _dbContext.Servicios.Where(p => p.IdServicio == dv.IdServicio).First();

                        _dbContext.Servicios.Update(servicio_encontrado);
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

        public async Task<bool> EditarCotizacion(Cotizacion modelo)
        {

            //CREAMOS UNA VARIABLE
            Cotizacion cotizacionGenerada = new();

            Prospecto prospecto = new Prospecto();

            //1-TRANSFORMAR EL DTO DE COTIZACION


            //si dentro de la logica ocurre un error la linea siguiente tiene que reestablecer todo al principio
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    

                    var cotizacionModelo = modelo;

                    var cotizacionEncontrado = _dbContext.Cotizacions.Where(p => p.IdCotizacion == modelo.IdCotizacion).First();

                    var serviciosAnteriores= _dbContext.CotizacionServicios.Where(p => p.IdCotizacion == modelo.IdCotizacion).First();



                    foreach (CotizacionServicio di in modelo.CotizacionServicios)
                    {
                            Servicio servicio_reescribir = _dbContext.Servicios.Where(p => p.IdServicio == di.IdServicio).First();

                            _dbContext.Servicios.Update(servicio_reescribir);
                    }
                    await _dbContext.SaveChangesAsync();

                    //var cotizacionEncontrado = await _cotizacionRepositorio.Obtener(u => u.IdCotizacion == cotizacionModelo.IdCotizacion);

                    if (cotizacionEncontrado == null)
                    {
                        throw new TaskCanceledException("No existe el COTIZACION");
                    }
                    cotizacionEncontrado.IdProspecto = cotizacionModelo.IdProspecto;
                    cotizacionEncontrado.Pulgadas2 = cotizacionModelo.Pulgadas2;
                    cotizacionEncontrado.TipoPago = cotizacionModelo.TipoPago;
                    cotizacionEncontrado.Total = cotizacionModelo.Total;
                    cotizacionEncontrado.TiempoFinancing = cotizacionModelo.TiempoFinancing;
                    cotizacionEncontrado.Ahorra = cotizacionModelo.Ahorra;
                    cotizacionEncontrado.ValorIntereses = cotizacionModelo.ValorIntereses;
                    cotizacionEncontrado.CuotaMensual = cotizacionModelo.CuotaMensual;
                    cotizacionEncontrado.PagoElectricidad = cotizacionModelo.PagoElectricidad;
                    cotizacionEncontrado.PorcentajeIncremento = cotizacionModelo.PorcentajeIncremento;
                    cotizacionEncontrado.MensualAprox = cotizacionModelo.MensualAprox;
                    cotizacionEncontrado.ValorPagado = cotizacionModelo.ValorPagado;
                    cotizacionEncontrado.ProyeccionSolar = cotizacionModelo.ProyeccionSolar;
                    cotizacionEncontrado.TiempoSolar = cotizacionModelo.TiempoSolar;
                    cotizacionEncontrado.Contrato = cotizacionModelo.Contrato;
                    cotizacionEncontrado.Email = cotizacionModelo.Email;
                    cotizacionEncontrado.TipoIdentificacion = cotizacionModelo.TipoIdentificacion;
                    cotizacionEncontrado.Identificacion = cotizacionModelo.Identificacion;
                    cotizacionEncontrado.SocEin = cotizacionModelo.SocEin;
                    cotizacionEncontrado.TamanoSistema = cotizacionModelo.TamanoSistema;
                    cotizacionEncontrado.PagoInicial = cotizacionModelo.PagoInicial;
                    cotizacionEncontrado.Notas = cotizacionModelo.Notas;
                    cotizacionEncontrado.Url1 = cotizacionModelo.Url1;
                    cotizacionEncontrado.Url2 = cotizacionModelo.Url2;
                    cotizacionEncontrado.Url3 = cotizacionModelo.Url3;
                    cotizacionEncontrado.Url4 = cotizacionModelo.Url4;
                    cotizacionEncontrado.Pdf = cotizacionModelo.Pdf;


                   

                   
                    //se cayo aqui- probar si fue por l abd esta sola

                    var idProspecto = Convert.ToInt32(modelo.IdProspecto);

                    Prospecto prospecto_encontrado = _dbContext.Prospectos.Where(p => p.IdProspecto == modelo.IdProspecto).First();

                    modelo.IdProspectoNavigation = prospecto_encontrado;

                    cotizacionEncontrado.FechaRegistro  = DateTime.Now;

                    _dbContext.Set<Cotizacion>().Update(cotizacionEncontrado);
                    await _dbContext.SaveChangesAsync();

                    /*await _dbContext.AddAsync(modelo);
                    await _dbContext.SaveChangesAsync();
                    */
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
                return true;
            }
        }

       /* public async  Task<Cotizacion> ListaServicios()
        {
            try
            {
                TModelo modelo = await _dbcontext.Set<TModelo>().Where(filtro).FirstOrDefaultAsync();
                return modelo;
            }
            catch { throw; }
        }*/
    }
}
