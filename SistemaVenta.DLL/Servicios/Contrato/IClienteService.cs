using SistemaVenta.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVenta.Models;

namespace SistemaVenta.DLL.Servicios.Contrato
{
    public interface IClienteService
    {
        Task<ClienteDTO> Registrar(ClienteDTO modelo);

        Task<ClienteDTO> RegistrarCliente(ClienteDTO modelo);
    }
}
