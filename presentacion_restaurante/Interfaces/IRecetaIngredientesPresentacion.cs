using System;
using System.Collections.Generic;
using System.Text;
using lib_aplicaciones.Entidades;

namespace presentacion_restaurante.Interfaces
{
    public interface IRecetaIngredientesPresentacion
    {
        RecetaIngredientes Insertar(RecetaIngredientes entidad);
        RecetaIngredientes Actualizar(RecetaIngredientes entidad);
        List<RecetaIngredientes> Consultar();
        List<RecetaIngredientes> Buscar(RecetaIngredientes entidad);
        RecetaIngredientes Borrar(RecetaIngredientes entidad);
    }
}
