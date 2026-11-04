using System;
using System.Collections.Generic;
using System.Text;
using lib_aplicaciones.Entidades;

namespace presentacion_restaurante.Interfaces
{
    public interface IDetallePedidosPresentacion
    {
        DetallePedidos Insertar(DetallePedidos entidad);
        DetallePedidos Actualizar(DetallePedidos entidad);
        List<DetallePedidos> Consultar();
        List<DetallePedidos> Buscar(DetallePedidos entidad);
        DetallePedidos Borrar(DetallePedidos entidad);
    }
}
