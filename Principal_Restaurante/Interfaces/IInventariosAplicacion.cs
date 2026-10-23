using System;
using System.Collections.Generic;
using System.Text;
using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IInventariosAplicacion
    {
        Inventarios Insertar(Inventarios entidad);
        Inventarios Actualizar(Inventarios entidad);
        List<Inventarios> Consultar();
        List<Inventarios> Buscar(Inventarios entidad);
        Inventarios Borrar(Inventarios entidad);

        //Inventarios Validar(Inventarios entidad);
    }
}
