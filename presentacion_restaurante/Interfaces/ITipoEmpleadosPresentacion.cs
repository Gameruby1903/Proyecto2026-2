using System;
using System.Collections.Generic;
using System.Text;
using lib_aplicaciones.Entidades;

namespace presentacion_restaurante.Interfaces
{
    public interface ITipoEmpleadosPresentacion
    {
        TipoEmpleados Insertar(TipoEmpleados entidad);
        TipoEmpleados Actualizar(TipoEmpleados entidad);
        List<TipoEmpleados> Consultar();
        List<TipoEmpleados> Buscar(TipoEmpleados entidad);
        TipoEmpleados Borrar(TipoEmpleados entidad);
    }
}
