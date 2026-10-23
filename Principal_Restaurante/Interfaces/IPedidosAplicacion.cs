using System;
using System.Collections.Generic;
using System.Text;
using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IPedidosAplicacion
    {
        Pedidos Insertar(Pedidos entidad);
        Pedidos Actualizar(Pedidos entidad);
        List<Pedidos> Consultar();
        List<Pedidos> Buscar(Pedidos entidad);
        Pedidos Borrar(Pedidos entidad);

        //Pedidos Validar(Pedidos entidad);
    }
}
