using System;
using System.Collections.Generic;
using System.Text;
using lib_aplicaciones.Entidades;

namespace presentacion_restaurante.Interfaces
{
    public interface IFacturasPresentacion
    {
        Facturas Insertar(Facturas entidad);
        Facturas Actualizar(Facturas entidad);
        List<Facturas> Consultar();
        List<Facturas> Buscar(Facturas entidad);
        Facturas Borrar(Facturas entidad);
    }
}
