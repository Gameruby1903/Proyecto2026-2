using System;
using System.Collections.Generic;
using System.Text;
using lib_aplicaciones.Entidades;

namespace presentacion_restaurante.Interfaces
{
    public interface IResenasPresentacion
    {
        Resenas Insertar(Resenas entidad);
        Resenas Actualizar(Resenas entidad);
        List<Resenas> Consultar();
        List<Resenas> Buscar(Resenas entidad);
        Resenas Borrar(Resenas entidad);
    }
}
