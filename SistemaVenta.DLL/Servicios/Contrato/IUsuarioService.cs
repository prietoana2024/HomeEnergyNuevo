﻿using SistemaVenta.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DLL.Servicios.Contrato
{
    public interface IUsuarioService
    {
        Task<List<UsuarioDTO>> Lista();
        Task<SesionDTO> ValidarCredenciales(string Correo, string Clave);
        Task<UsuarioDTO> Crear(UsuarioDTO modelo);
        Task<bool> Editar(UsuarioDTO modelo);
        Task<bool> Eliminar(int id);
    }
}
