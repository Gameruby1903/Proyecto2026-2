using System;
using System.Collections.Generic;
using System.Text;
using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface ISucursalesAplicacion
    {
        Sucursales Insertar(Sucursales entidad);
        Sucursales Actualizar(Sucursales entidad);
        List<Sucursales> Consultar();
        List<Sucursales> Buscar(Sucursales entidad);
        Sucursales Borrar(Sucursales entidad);

        //Sucursales Validar(Sucursales entidad);
    }
}
