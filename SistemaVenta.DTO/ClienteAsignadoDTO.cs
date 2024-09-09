using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DTO
{
    public class ClienteAsignadoDTO
    {
        public int? IdCliente { get; set; }

        public string? DescripcionCliente { get; set; }

        public string? Fachadaimg { get; set; }

        public string? Url { get; set; }

        public int? IdUsuario { get; set; }
        public string? Contacto { get; set; }

        public int? Idauditor { get; set; }
        public string? Usuario { get; set; }

        public string? FechaRegistro { get; set; }

        public string? Fecha { get; set; }

        /*
        public string? numeroDocumento { get; set; }
        public string? TipoPago { get; set; }
        public string? FechaRegistro { get; set; }

        public string? TotalVenta { get; set; }

        public string? Servicio { get; set; }

        public int? Cantidad { get; set; }

        public string? Precio { get; set; }

        public string? Total { get; set; }*/
    }
}
