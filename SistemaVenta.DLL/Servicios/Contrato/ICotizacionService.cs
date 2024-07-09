using SistemaVenta.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DLL.Servicios.Contrato
{
    public interface ICotizacionService
    {
        Task<List<CotizacionDTO>> Lista();
        Task<CotizacionDTO> Crear(CotizacionDTO modelo);
        Task<bool> Editar(CotizacionDTO modelo);
        Task<bool> Eliminar(int id);
    }
}
