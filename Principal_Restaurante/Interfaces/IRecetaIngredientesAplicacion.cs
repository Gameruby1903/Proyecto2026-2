using System;
using System.Collections.Generic;
using System.Text;
using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IRecetaIngredientesAplicacion
    {
        RecetaIngredientes Insertar(RecetaIngredientes entidad);
        RecetaIngredientes Actualizar(RecetaIngredientes entidad);
        List<RecetaIngredientes> Consultar();
        List<RecetaIngredientes> Buscar(RecetaIngredientes entidad);
        RecetaIngredientes Borrar(RecetaIngredientes entidad);

        //RecetaIngredientes Validar(RecetaIngredientes entidad);
    }
}
