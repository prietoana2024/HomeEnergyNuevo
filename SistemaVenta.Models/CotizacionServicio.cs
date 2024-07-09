using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.Models
{
    public class CotizacionServicio
    {
        public int IdCotizacionServicio { get; set; }

        public int? IdCotizacion { get; set; }

        public int? IdServicio { get; set; }

        public virtual Cotizacion? IdCotizacionNavigation { get; set; }

        public virtual Servicio? IdServicioNavigation { get; set; }
    }
}
