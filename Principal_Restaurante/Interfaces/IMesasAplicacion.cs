using System;
using System.Collections.Generic;
using System.Text;
using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IMesasAplicacion
    {
        Mesas Insertar(Mesas entidad);
        Mesas Actualizar(Mesas entidad);
        List<Mesas> Consultar();
        List<Mesas> Buscar(Mesas entidad);
        Mesas Borrar(Mesas entidad);

        //Mesas Validar(Mesas entidad);
    }
}
