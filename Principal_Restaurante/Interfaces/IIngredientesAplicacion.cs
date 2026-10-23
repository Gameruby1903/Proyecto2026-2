using System;
using System.Collections.Generic;
using System.Text;
using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IIngredientesAplicacion
    {
        Ingredientes Insertar(Ingredientes entidad);
        Ingredientes Actualizar(Ingredientes entidad);
        List<Ingredientes> Consultar();
        List<Ingredientes> Buscar(Ingredientes entidad);
        Ingredientes Borrar(Ingredientes entidad);

        //Ingredientes Validar(Ingredientes entidad);
    }
}
