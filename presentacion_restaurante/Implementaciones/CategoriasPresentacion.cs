
using System.Collections.Generic;
using System.Text;
using presentacion_restaurante.Interfaces;
using lib_aplicaciones.Entidades;

namespace presentacion_restaurante.Implementaciones
{
    public class CategoriasPresentacion : ICategoriasPresentacion
    {

        private IComunicaciones IComunicaciones;

        public CategoriasPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public Categorias Actualizar(Categorias entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Categorias>(
                "Categorias/Actualizar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public Categorias Borrar(Categorias entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Categorias>(
                "Categorias/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Categorias> Buscar(Categorias entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Categorias>>(
                "Categorias/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Categorias> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Categorias>>(
                "Categorias/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

        public Categorias Insertar(Categorias entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Categorias>(
                "Categorias/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

        private bool Validar(Categorias entidad)
        {
            return string.IsNullOrEmpty(entidad.Nombre) ||
                string.IsNullOrEmpty(entidad.Descripcion) ||
                entidad.Disponible == true ||
                entidad.OrdenMenu != 0;
        }
    }
}

