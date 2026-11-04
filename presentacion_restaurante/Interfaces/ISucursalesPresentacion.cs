using System;
using System.Collections.Generic;
using System.Text;
using lib_aplicaciones.Entidades;

namespace presentacion_restaurante.Interfaces
{
    public interface ISucursalesPresentacion
    {
        Sucursales Insertar(Sucursales entidad);
        Sucursales Actualizar(Sucursales entidad);
        List<Sucursales> Consultar();
        List<Sucursales> Buscar(Sucursales entidad);
        Sucursales Borrar(Sucursales entidad);
    }
}
