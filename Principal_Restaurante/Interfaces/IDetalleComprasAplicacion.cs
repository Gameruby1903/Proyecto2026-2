using System;
using System.Collections.Generic;
using System.Text;
using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IDetalleComprasAplicacion
    {
        DetalleCompras Insertar(DetalleCompras entidad);
        DetalleCompras Actualizar(DetalleCompras entidad);
        List<DetalleCompras> Consultar();
        List<DetalleCompras> Buscar(DetalleCompras entidad);
        DetalleCompras Borrar(DetalleCompras entidad);

        //DetalleCompras Validar(DetalleCompras entidad);
    }
}
