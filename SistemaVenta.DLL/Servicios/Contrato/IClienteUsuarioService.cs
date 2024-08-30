using SistemaVenta.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DLL.Servicios.Contrato
{
    public interface IClienteUsuarioService
    {
        Task<List<ClienteUsuarioDTO>> Lista();
        Task<ClienteUsuarioDTO> Crear(ClienteUsuarioDTO modelo);
        Task<bool> Editar(ClienteUsuarioDTO modelo);
        Task<bool> Eliminar(int id);
    }
}
