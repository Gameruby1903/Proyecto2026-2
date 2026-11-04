using System;
using System.Collections.Generic;
using System.Text;
using lib_aplicaciones.Entidades;

namespace presentacion_restaurante.Interfaces
{
    public interface IMetodoPagosPresentacion
    {
        MetodoPagos Insertar(MetodoPagos entidad);
        MetodoPagos Actualizar(MetodoPagos entidad);
        List<MetodoPagos> Consultar();
        List<MetodoPagos> Buscar(MetodoPagos entidad);
        MetodoPagos Borrar(MetodoPagos entidad);
    }
}
