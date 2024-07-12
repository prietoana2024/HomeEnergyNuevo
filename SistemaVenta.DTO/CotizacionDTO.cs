using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DTO
{
    public class CotizacionDTO
    {
        public int? IdCotizacion { get; set; }

        public int? IdProspecto { get; set; }

        public string? DescripcionProspecto { get; set; }

        public int? Pulgadas2 { get; set; }

        public string? TipoPago { get; set; }

        public string? TotalTexto { get; set; }

        public int? TiempoFinancing { get; set; }

        public string? AhorraTexto { get; set; }

        public string? ValorInteresesTexto { get; set; }

        public string? CuotaMensualTexto { get; set; }

        public string? PagoElectricidadTexto { get; set; }

        public int? PorcentajeIncremento { get; set; }

        public string? MensualAproxTexto { get; set; }

        public string? ValorPagadoTexto { get; set; }

        public string? ProyeccionSolarTexto { get; set; }

        public int? TiempoSolar { get; set; }

        public string? Contrato { get; set; }

        public string? Email { get; set; }

        public string? TipoIdentificacion { get; set; }

        public int? Identificacion { get; set; }

        public int? SocEin { get; set; }

        public string? TamanoSistema { get; set; }

        public string? PagoInicialTexto { get; set; }

        public string? Notas { get; set; }

        public string? Url1 { get; set; }

        public string? Url2 { get; set; }

        public string? Url3 { get; set; }

        public string? Url4 { get; set; }

        public string? Pdf { get; set; }

        public string? FechaRegistro { get; set; }

        public virtual ICollection<ServicioDTO> Servicios { get; set; }

    }
}
