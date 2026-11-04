using System;
using System.Collections.Generic;
using System.Text;
using lib_aplicaciones.Entidades;

namespace presentacion_restaurante.Interfaces
{
    public interface IIngredientesPresentacion
    {
        Ingredientes Insertar(Ingredientes entidad);
        Ingredientes Actualizar(Ingredientes entidad);
        List<Ingredientes> Consultar();
        List<Ingredientes> Buscar(Ingredientes entidad);
        Ingredientes Borrar(Ingredientes entidad);
    }
}
