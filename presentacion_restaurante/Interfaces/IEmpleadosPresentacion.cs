using System;
using System.Collections.Generic;
using System.Text;
using lib_aplicaciones.Entidades;

namespace presentacion_restaurante.Interfaces
{
    public interface IEmpleadosPresentacion
    {
        Empleados Insertar(Empleados entidad);
        Empleados Actualizar(Empleados entidad);
        List<Empleados> Consultar();
        List<Empleados> Buscar(Empleados entidad);
        Empleados Borrar(Empleados entidad);
    }
}
