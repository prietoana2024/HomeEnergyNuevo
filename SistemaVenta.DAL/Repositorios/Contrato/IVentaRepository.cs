﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVenta.DAL;
using SistemaVenta.Models;

namespace SistemaVenta.DAL.Repositorios.Contrato
{
    public interface IVentaRepository : IGenericRepository<Venta>
    {
        Task<Venta> Registrar(Venta modelo);

    }
}
