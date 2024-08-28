using SistemaVenta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DAL.Repositorios.Contrato
{
    public interface ICotizacionRepository : IGenericRepository<Cotizacion>
    {
        Task<Cotizacion> Registrar(Cotizacion modelo);

        Task<bool> EditarCotizacion(Cotizacion modelo);

       // Task<Cotizacion> ListaServicios();

    }
}
