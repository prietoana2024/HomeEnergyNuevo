﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DTO
{
    public class ClientesXUsuarioDTO
    {
        public int IdCliente { get; set; }

        public string? Nombre { get; set; }

        public string? Fachadaimg { get; set; }

        public string? Url { get; set; }

        public string? Direccion { get; set; }

        public string? Contacto { get; set; }

        public string? RazonSocial { get; set; }

        public int? Idauditor { get; set; }

        public string? Detalle { get; set; }

        public bool? EsActivo { get; set; }

        public DateTime? Fecha { get; set; }

        public int? IdProspecto { get; set; }

        public DateTime? FechaRegistro { get; set; }


        public int IdClienteUsuario { get; set; }

       // public int IdCliente { get; set; }

        public int IdUsuario { get; set; }

        public string? NombreCompleto { get; set; }

        public string? Correo { get; set; }

        public int? IdRol { get; set; }

        public string? Clave { get; set; }

      //  public int? EsActivo { get; set; }



    }
}
