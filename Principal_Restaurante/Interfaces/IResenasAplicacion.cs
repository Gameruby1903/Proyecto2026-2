using System;
using System.Collections.Generic;
using System.Text;
using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IResenasAplicacion
    {
        Resenas Insertar(Resenas entidad);
        Resenas Actualizar(Resenas entidad);
        List<Resenas> Consultar();
        List<Resenas> Buscar(Resenas entidad);
        Resenas Borrar(Resenas entidad);

        //Resenas Validar(Resenas entidad);
    }
}
