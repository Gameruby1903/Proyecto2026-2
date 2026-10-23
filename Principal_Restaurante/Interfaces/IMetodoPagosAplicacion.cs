using System;
using System.Collections.Generic;
using System.Text;
using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IMetodoPagosAplicacion
    {
        MetodoPagos Insertar(MetodoPagos entidad);
        MetodoPagos Actualizar(MetodoPagos entidad);
        List<MetodoPagos> Consultar();
        List<MetodoPagos> Buscar(MetodoPagos entidad);
        MetodoPagos Borrar(MetodoPagos entidad);

        //MetodoPagos Validar(MetodoPagos entidad);
    }
}
