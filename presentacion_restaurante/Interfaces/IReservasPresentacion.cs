using System;
using System.Collections.Generic;
using System.Text;
using lib_aplicaciones.Entidades;

namespace presentacion_restaurante.Interfaces
{
    public interface IReservasPresentacion
    {
        Reservas Insertar(Reservas entidad);
        Reservas Actualizar(Reservas entidad);
        List<Reservas> Consultar();
        List<Reservas> Buscar(Reservas entidad);
        Reservas Borrar(Reservas entidad);
    }
}
