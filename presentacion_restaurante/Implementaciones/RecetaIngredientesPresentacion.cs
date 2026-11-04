
using System.Collections.Generic;
using System.Text;
using presentacion_restaurante.Interfaces;
using lib_aplicaciones.Entidades;

namespace presentacion_restaurante.Implementaciones
{
    public class RecetaIngredientesPresentacion : IRecetaIngredientesPresentacion
    {

        private IComunicaciones IComunicaciones;

        public RecetaIngredientesPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public RecetaIngredientes Actualizar(RecetaIngredientes entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<RecetaIngredientes>(
                "RecetaIngredientes/Actualizar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public RecetaIngredientes Borrar(RecetaIngredientes entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<RecetaIngredientes>(
                "RecetaIngredientes/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<RecetaIngredientes> Buscar(RecetaIngredientes entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<RecetaIngredientes>>(
                "RecetaIngredientes/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<RecetaIngredientes> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<RecetaIngredientes>>(
                "RecetaIngredientes/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

        public RecetaIngredientes Insertar(RecetaIngredientes entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<RecetaIngredientes>(
                "RecetaIngredientes/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

        private bool Validar(RecetaIngredientes entidad)
        {
            return 
                string.IsNullOrEmpty(entidad.Notas) ||
                entidad.CantiReque != 0;
        }
    }
}

