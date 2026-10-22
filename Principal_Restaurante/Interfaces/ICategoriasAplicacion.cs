using System;
using System.Collections.Generic;
using System.Text;
using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface ICategoriasAplicacion
    {
        Categorias Insertar(Categorias entidad);
        Categorias Actualizar(Categorias entidad);
        List<Categorias> Consultar();
        List<Categorias> Buscar(Categorias entidad);
        Categorias Borrar(Categorias entidad);

        //Categorias Validar(Categorias entidad);
    }
}
