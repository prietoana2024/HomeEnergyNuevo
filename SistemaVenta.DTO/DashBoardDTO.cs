﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DTO
{
    public class DashBoardDTO
    {
        public int? TotalVentas { get; set; }

        public string? TotalIngresos { get; set; }

        public int? TotalServicios { get; set; }
        public int? TotalCerradas { get; set; }
        public List<VentasSemanaDTO> VentasUltimaSemana { get; set; }
    }
}
