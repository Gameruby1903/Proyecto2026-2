using System;
using System.Collections.Generic;
using System.Text;
using lib_aplicaciones.Entidades;

namespace presentacion_restaurante.Interfaces
{
    public interface IInventariosPresentacion
    {
        Inventarios Insertar(Inventarios entidad);
        Inventarios Actualizar(Inventarios entidad);
        List<Inventarios> Consultar();
        List<Inventarios> Buscar(Inventarios entidad);
        Inventarios Borrar(Inventarios entidad);
    }
}
