using System;
using System.Collections.Generic;
using System.Text;
using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IDetallePedidosAplicacion
    {
        DetallePedidos Insertar(DetallePedidos entidad);
        DetallePedidos Actualizar(DetallePedidos entidad);
        List<DetallePedidos> Consultar();
        List<DetallePedidos> Buscar(DetallePedidos entidad);
        DetallePedidos Borrar(DetallePedidos entidad);

        //DetallePedidos Validar(DetallePedidos entidad);
    }
}
