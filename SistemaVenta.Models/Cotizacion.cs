using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.Models
{
    public class Cotizacion
    {
        public int IdCotizacion { get; set; }

        public int? IdProspecto { get; set; }

        public int? Pulgadas2 { get; set; }

        public string? TipoPago { get; set; }

        public decimal? Total { get; set; }

        public int? TiempoFinancing { get; set; }

        public decimal? Ahorra { get; set; }

        public decimal? ValorIntereses { get; set; }

        public decimal? CuotaMensual { get; set; }

        public decimal? PagoElectricidad { get; set; }

        public int? PorcentajeIncremento { get; set; }

        public decimal? MensualAprox { get; set; }

        public decimal? ValorPagado { get; set; }

        public decimal? ProyeccionSolar { get; set; }

        public int? TiempoSolar { get; set; }

        public string? Contrato { get; set; }

        public string? Email { get; set; }

        public string? TipoIdentificacion { get; set; }

        public int? Identificacion { get; set; }

        public int? SocEin { get; set; }

        public string? TamanoSistema { get; set; }

        public decimal? PagoInicial { get; set; }

        public string? Notas { get; set; }

        public string? Url1 { get; set; }

        public string? Url2 { get; set; }

        public string? Url3 { get; set; }

        public string? Url4 { get; set; }

        public string? Pdf { get; set; }

        public DateTime? FechaRegistro { get; set; }

        public virtual ICollection<CotizacionServicio> CotizacionServicios { get; } = new List<CotizacionServicio>();

        public virtual Prospecto? IdProspectoNavigation { get; set; }
    }
}
