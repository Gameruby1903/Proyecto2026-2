using System;
using System.Collections.Generic;
using System.Text;
using lib_aplicaciones.Entidades;

namespace presentacion_restaurante.Interfaces
{
    public interface IComprasPresentacion
    {
        Compras Insertar(Compras entidad);
        Compras Actualizar(Compras entidad);
        List<Compras> Consultar();
        List<Compras> Buscar(Compras entidad);
        Compras Borrar(Compras entidad);
    }
}
