using System;
using System.Collections.Generic;
using System.Text;
using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface ITipoEmpleadosAplicacion
    {
        TipoEmpleados Insertar(TipoEmpleados entidad);
        TipoEmpleados Actualizar(TipoEmpleados entidad);
        List<TipoEmpleados> Consultar();
        List<TipoEmpleados> Buscar(TipoEmpleados entidad);
        TipoEmpleados Borrar(TipoEmpleados entidad);

        //TipoEmpleados Validar(TipoEmpleados entidad);
    }
}
