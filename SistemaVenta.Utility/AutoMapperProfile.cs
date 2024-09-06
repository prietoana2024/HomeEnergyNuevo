using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using SistemaVenta.DTO;
using SistemaVenta.Models;

namespace SistemaVenta.Utility
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Rol
            CreateMap<Rol, RolDTO>().ReverseMap();
            #endregion Rol


            #region Menu
            CreateMap<Menu, MenuDTO>().ReverseMap();
            #endregion Menu


            #region Usuario
            CreateMap<Usuario, UsuarioDTO>().ForMember(destino => destino.RolDescripcion, opt => opt.MapFrom(origen => origen.IdRolNavigation.Nombre))
            .ForMember(destino =>
            destino.EsActivo,
            opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0));

            CreateMap<Usuario, SesionDTO>()
                .ForMember(destino =>
                destino.RolDescripcion,
                opt => opt.MapFrom(origen => origen.IdRolNavigation.Nombre));

            CreateMap<UsuarioDTO, Usuario>()
                .ForMember(destino => destino.IdRolNavigation, opt => opt.Ignore()
                )
                .ForMember(destino =>
            destino.EsActivo,
            opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false));
            #endregion Usuario

            #region Categoria
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
            #endregion Categoria

            #region Servicio
            CreateMap<Servicio, ServicioDTO>()
                .ForMember(destino => destino.DescripcionCategoria,
                opt => opt.MapFrom(origen => origen.IdCategoriaNavigation.Nombre)
                )
                .ForMember(destino => destino.Precio,
                opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-CO")))
                )
                .ForMember(destino =>
                destino.EsActivo,
                opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0));

            CreateMap<ServicioDTO, Servicio>()
                .ForMember(destino => destino.IdCategoriaNavigation,
                opt => opt.Ignore()
                )
                .ForMember(destino => destino.Precio,
                opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Precio, new CultureInfo("es-CO")))
                )
                .ForMember(destino =>
                destino.EsActivo,
                opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false));
            #endregion Servicio

            #region Venta
            CreateMap<Venta, VentaDTO>()
                .ForMember(destino =>
                destino.TotalTexto,
                opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-CO")))
                )
                .ForMember(destino =>
                destino.FechaRegistro,
                opt => opt.MapFrom(origen => origen.FechaRegistro.Value.ToString("dd/MM/yyyy"))
                );
            CreateMap<VentaDTO, Venta>()
                .ForMember(destino =>
                destino.Total,
                opt => opt.MapFrom(origen => Convert.ToDecimal(origen.TotalTexto, new CultureInfo("es-CO")))
                )
                .ForMember(destino => destino.FechaRegistro,
                opt => opt.MapFrom(origen => Convert.ToDateTime(origen.FechaRegistro)));

            #endregion

            #region DetalleVenta
            CreateMap<DetalleVenta, DetalleVentaDTO>()
                .ForMember(destino => destino.DescripcionServicio,
                opt => opt.MapFrom(origen => origen.IdServicioNavigation.Nombre)
                )
                .ForMember(destino => destino.EstadoDescripcion,
                opt => opt.MapFrom(origen => origen.IdEstadoNavigation.Nombre)
                )
                .ForMember(destino =>
                destino.PrecioTexto,
                opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-CO"))
                ))
                .ForMember(destino =>
                destino.TotalTexto,
                opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-CO")))
                );
            CreateMap<DetalleVentaDTO, DetalleVenta>()
                .ForMember(destino => destino.IdEstadoNavigation,
                opt => opt.Ignore()
                )
                .ForMember(destino =>
                destino.Precio,
                opt => opt.MapFrom(origen => Convert.ToDecimal(origen.PrecioTexto, new CultureInfo("es-CO"))
                ))
                .ForMember(destino =>
                destino.Total,
                opt => opt.MapFrom(origen => Convert.ToDecimal(origen.TotalTexto, new CultureInfo("es-CO")))
                );
            CreateMap<Estado, DetalleVentaDTO>()
                .ForMember(destino =>
                destino.EstadoDescripcion,
                opt => opt.MapFrom(origen => origen.Nombre));

            #endregion

            #region Reporte
            CreateMap<DetalleVenta, ReporteDTO>()
                .ForMember(destino => destino.FechaRegistro,
                opt => opt.MapFrom(origen => origen.IdVentaNavigation.FechaRegistro.Value.ToString("dd/MM/yyyy")))

               .ForMember(destino => destino.numeroDocumento,
                opt => opt.MapFrom(origen => origen.IdVentaNavigation.NumeroDocumento))

               .ForMember(destino => destino.TipoPago,
                opt => opt.MapFrom(origen => origen.IdVentaNavigation.TipoPago))

               .ForMember(destino =>
                destino.TotalVenta,
                opt => opt.MapFrom(origen => Convert.ToString(origen.IdVentaNavigation.Total.Value, new CultureInfo("es-CO"))))

                .ForMember(destino =>
                destino.Servicio,
                opt => opt.MapFrom(origen => Convert.ToString(origen.IdServicioNavigation.Nombre)))

                .ForMember(destino =>
                destino.Precio,
                opt => opt.MapFrom(origen => Convert.ToString(origen.Precio, new CultureInfo("es-CO"))))


                .ForMember(destino =>
                destino.Total,
                opt => opt.MapFrom(origen => Convert.ToString(origen.Total, new CultureInfo("es-CO"))));



            #endregion

            #region Prospecto
            CreateMap<Prospecto, ProspectoDTO>()
            .ForMember(destino =>
                destino.EsActivo,
                opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0));

            CreateMap<ProspectoDTO, Prospecto>()
                 .ForMember(destino =>
                destino.EsActivo,
                opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false));

            #endregion Prospecto

            #region Estado
            CreateMap<Estado, EstadoDTO>().ReverseMap();
            #endregion Estado

            #region FileData
            CreateMap<FileData, FileDataDTO>()
                .ForMember(destino => destino.Image,
                opt => opt.MapFrom(origen => origen.IdImagenNavigation.Nombre)
                );

            CreateMap<FileDataDTO, FileData>()
                .ForMember(destino => destino.IdImagenNavigation,
                opt => opt.Ignore()
                );
            #endregion FileData

            #region FileRecord
            CreateMap<FileData, FileRecordDTO>()
                .ForMember(destino => destino.FileFormat,
                opt => opt.MapFrom(origen => origen.Extension)
                ).ForMember(destino => destino.ContentType,
                opt => opt.MapFrom(origen => origen.MimeType)
                );

            CreateMap<FileRecordDTO, FileData>()
                .ForMember(destino => destino.Extension,
                opt => opt.MapFrom(origen => origen.FileFormat))
                .ForMember(destino => destino.MimeType,
                opt => opt.MapFrom(origen => origen.ContentType)
                );
            #endregion FileRecord

            #region Cotizacion
            CreateMap<Cotizacion, CotizacionDTO>()
                .ForMember(destino => destino.DescripcionProspecto,
                opt => opt.MapFrom(origen => origen.IdProspectoNavigation.Nombre)
                )
                .ForMember(destino => destino.TotalTexto,
                opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Total.Value, new CultureInfo("es-CO")))
                )
                .ForMember(destino => destino.AhorraTexto,
                opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Ahorra.Value, new CultureInfo("es-CO")))
                )
                .ForMember(destino => destino.ValorInteresesTexto,
                opt => opt.MapFrom(origen => Convert.ToDecimal(origen.ValorIntereses.Value, new CultureInfo("es-CO")))
                )
                 .ForMember(destino => destino.CuotaMensualTexto,
                opt => opt.MapFrom(origen => Convert.ToDecimal(origen.CuotaMensual.Value, new CultureInfo("es-CO")))
                )
                 .ForMember(destino => destino.PagoElectricidadTexto,
                opt => opt.MapFrom(origen => Convert.ToDecimal(origen.PagoElectricidad.Value, new CultureInfo("es-CO")))
                )
                 .ForMember(destino => destino.MensualAproxTexto,
                opt => opt.MapFrom(origen => Convert.ToDecimal(origen.MensualAprox.Value, new CultureInfo("es-CO")))
                )
                  .ForMember(destino => destino.ValorPagadoTexto,
                opt => opt.MapFrom(origen => Convert.ToDecimal(origen.ValorPagado.Value, new CultureInfo("es-CO")))
                )
                  .ForMember(destino => destino.ProyeccionSolarTexto,
                opt => opt.MapFrom(origen => Convert.ToDecimal(origen.ProyeccionSolar.Value, new CultureInfo("es-CO")))
                )
                  .ForMember(destino => destino.PagoInicialTexto,
                opt => opt.MapFrom(origen => Convert.ToDecimal(origen.PagoInicial.Value, new CultureInfo("es-CO")))
                )
                .ForMember(destino =>
                destino.FechaRegistro,
                opt => opt.MapFrom(origen => origen.FechaRegistro.Value.ToString("dd/MM/yyyy"))
                )
                .ForMember(destino => destino.Servicios,
                 opt => opt.MapFrom(origen => origen.CotizacionServicios));



            CreateMap<CotizacionDTO, Cotizacion>()


           .ForMember(destino => destino.CotizacionServicios,
             opt => opt.MapFrom(origen => origen.Servicios))

           .ForMember(destino => destino.Total,
           opt => opt.MapFrom(origen => Convert.ToDecimal(origen.TotalTexto, new CultureInfo("es-CO"))))
           .ForMember(destino => destino.Ahorra,
           opt => opt.MapFrom(origen => Convert.ToDecimal(origen.AhorraTexto, new CultureInfo("es-CO"))))
           .ForMember(destino => destino.ValorIntereses,
           opt => opt.MapFrom(origen => Convert.ToDecimal(origen.ValorInteresesTexto, new CultureInfo("es-CO"))))
           .ForMember(destino => destino.CuotaMensual,
           opt => opt.MapFrom(origen => Convert.ToDecimal(origen.CuotaMensualTexto, new CultureInfo("es-CO"))))
           .ForMember(destino => destino.PagoElectricidad,
           opt => opt.MapFrom(origen => Convert.ToDecimal(origen.PagoElectricidadTexto, new CultureInfo("es-CO"))))
           .ForMember(destino => destino.MensualAprox,
           opt => opt.MapFrom(origen => Convert.ToDecimal(origen.MensualAproxTexto, new CultureInfo("es-CO"))))
           .ForMember(destino => destino.ValorPagado,
           opt => opt.MapFrom(origen => Convert.ToDecimal(origen.ValorPagadoTexto, new CultureInfo("es-CO"))))
           .ForMember(destino => destino.ProyeccionSolar,
           opt => opt.MapFrom(origen => Convert.ToDecimal(origen.ProyeccionSolarTexto, new CultureInfo("es-CO"))))
           .ForMember(destino => destino.PagoInicial,
           opt => opt.MapFrom(origen => Convert.ToDecimal(origen.PagoInicialTexto, new CultureInfo("es-CO"))))
           .ForMember(destino => destino.FechaRegistro,
           opt => opt.MapFrom(origen => Convert.ToDateTime(origen.FechaRegistro)));

            #endregion Cotizacion

            #region Cliente

            CreateMap<Cliente, ClienteDTO>()
                .ForMember(destino =>
                destino.EsActivo,
                opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0))

                 .ForMember(destino =>
                destino.Fecha,
                opt => opt.MapFrom(origen => origen.Fecha.Value.ToString("dd/MM/yyyy"))
                )
                .ForMember(destino =>
                destino.FechaRegistro,
                opt => opt.MapFrom(origen => origen.FechaRegistro.Value.ToString("dd/MM/yyyy"))
                )
                 .ForMember(destino => destino.IdProspecto,
                opt => opt.MapFrom(origen => origen.IdProspecto)
                )
                 .ForMember(destino => destino.IdProspecto,
                opt => opt.MapFrom(origen => origen.IdProspectoNavigation.IdProspecto)
                );
            //.ForMember(destino => destino.Usuarios, opt => opt.MapFrom(origen => origen.ClienteUsuarios));




            CreateMap<ClienteDTO, Cliente>()


                .ForMember(destino =>
                destino.EsActivo,
                opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false))

                .ForMember(destino => destino.Fecha,
                opt => opt.MapFrom(origen => Convert.ToDateTime(origen.Fecha)))

                .ForMember(destino => destino.FechaRegistro,
                opt => opt.MapFrom(origen => Convert.ToDateTime(origen.FechaRegistro)))

               .ForMember(destino => destino.IdProspecto,
                opt => opt.MapFrom(origen => origen.IdProspecto)
                )
               .ForMember(destino => destino.IdProspectoNavigation, opt => opt.Ignore()
                );
            //.ForMember(destino => destino.ClienteUsuarios, opt => opt.MapFrom(origen => origen.Usuarios));

            #endregion Cliente

            #region ClienteUsuario

            CreateMap<ClienteUsuario, ClienteUsuarioDTO>()
               .ForMember(destino => destino.IdUsuario,
              opt => opt.MapFrom(origen => origen.IdUsuarioNavigation.IdUsuario)
              )
               .ForMember(destino => destino.IdCliente,
              opt => opt.MapFrom(origen => origen.IdClienteNavigation.IdCliente)
              );

            CreateMap<ClienteUsuarioDTO, ClienteUsuario>()

               .ForMember(destino => destino.IdUsuarioNavigation, opt => opt.Ignore()
                )
               .ForMember(destino => destino.IdClienteNavigation, opt => opt.Ignore()
                );


            #endregion ClienteUsuario
            /*
            CreateMap<ClienteUsuario, UsuarioDTO>()
                .ForMember(destino =>
                destino.IdUsuario,
                opt => opt.MapFrom(origen => origen.IdUsuario));

            CreateMap<UsuarioDTO, ClienteUsuario>()
                .ForMember(destino =>
                destino.IdCliente,
                opt => opt.MapFrom(origen => origen.IdUsuario));

            CreateMap<ClienteUsuario, ClienteDTO>()
                .ForMember(destino =>
                destino.IdCliente,
                opt => opt.MapFrom(origen => origen.IdCliente));

            CreateMap<ClienteDTO, ClienteUsuario>()
                .ForMember(destino =>
                destino.IdCliente,
                opt => opt.MapFrom(origen => origen.IdCliente));

            */
            CreateMap<CotizacionServicio, CotizacionDTO>()
                .ForMember(destino =>
                destino.IdCotizacion,
                opt => opt.MapFrom(origen => origen.IdServicio));

            CreateMap<CotizacionDTO, CotizacionServicio>()
                .ForMember(destino =>
                destino.IdCotizacion,
                opt => opt.MapFrom(origen => origen.IdCotizacion));

            CreateMap<CotizacionServicio, ServicioDTO>()
                .ForMember(destino =>
                destino.IdServicio,
                opt => opt.MapFrom(origen => origen.IdServicio));

            CreateMap<ServicioDTO, CotizacionServicio>()
                .ForMember(destino =>
                destino.IdCotizacion,
                opt => opt.MapFrom(origen => origen.IdServicio));

            #region ClienteUsuarioIner
            CreateMap<ClienteUsuario, ClienteUsuarioInerDTO>()
                .ForMember(destino => destino.DescripcionUsuario,
                opt => opt.MapFrom(origen => origen.IdUsuarioNavigation.NombreCompleto)
                )
                .ForMember(destino => destino.DescripcionCliente,
                opt => opt.MapFrom(origen => origen.IdClienteNavigation.Nombre)
                );
            CreateMap<ClienteUsuarioInerDTO, ClienteUsuario>()
                .ForMember(destino => destino.IdClienteNavigation,
                opt => opt.Ignore()
                )
                .ForMember(destino => destino.IdUsuarioNavigation,
                opt => opt.Ignore()
                );

            #endregion ClienteUsuarioIner

            #region Reporte
            CreateMap<ClienteUsuario, ClienteAsignadoDTO>()
                .ForMember(destino => destino.IdCliente,
                opt => opt.MapFrom(origen => origen.IdClienteNavigation.IdCliente))

                 .ForMember(destino => destino.DescripcionCliente,
                opt => opt.MapFrom(origen => origen.IdClienteNavigation.Nombre))


               .ForMember(destino => destino.Contacto,
                opt => opt.MapFrom(origen => origen.IdClienteNavigation.Contacto))


                  .ForMember(destino => destino.FechaRegistro,
                opt => opt.MapFrom(origen => origen.IdClienteNavigation.FechaRegistro.Value.ToString("dd/MM/yyyy")))

                 .ForMember(destino => destino.Fecha,
                opt => opt.MapFrom(origen => origen.IdClienteNavigation.Fecha.Value.ToString("dd/MM/yyyy")))

               .ForMember(destino => destino.Idauditor,
                opt => opt.MapFrom(origen => origen.IdClienteNavigation.Idauditor))

                .ForMember(destino =>
                destino.Usuario,
                opt => opt.MapFrom(origen => Convert.ToString(origen.IdUsuarioNavigation.NombreCompleto)));

            #endregion
        }

    }
}
