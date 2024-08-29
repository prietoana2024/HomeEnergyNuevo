using SistemaVenta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DAL.Repositorios.Contrato
{
    public interface IClienteRepository:IGenericRepository<Cliente>
    {
        Task<Cliente> Registrar(Cliente modelo);

        Task<Cliente> RegistrarCliente(Cliente modelo);
    }

}
