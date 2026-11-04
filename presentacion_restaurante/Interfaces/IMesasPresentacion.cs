using System;
using System.Collections.Generic;
using System.Text;
using lib_aplicaciones.Entidades;

namespace presentacion_restaurante.Interfaces
{
    public interface IMesasPresentacion
    {
        Mesas Insertar(Mesas entidad);
        Mesas Actualizar(Mesas entidad);
        List<Mesas> Consultar();
        List<Mesas> Buscar(Mesas entidad);
        Mesas Borrar(Mesas entidad);
    }
}
