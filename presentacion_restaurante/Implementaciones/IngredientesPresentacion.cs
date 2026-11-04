
using System.Collections.Generic;
using System.Text;
using presentacion_restaurante.Interfaces;
using lib_aplicaciones.Entidades;

namespace presentacion_restaurante.Implementaciones
{
    public class IngredientesPresentacion : IIngredientesPresentacion
    {

        private IComunicaciones IComunicaciones;

        public IngredientesPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public Ingredientes Actualizar(Ingredientes entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Ingredientes>(
                "Ingredientes/Actualizar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public Ingredientes Borrar(Ingredientes entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Ingredientes>(
                "Ingredientes/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Ingredientes> Buscar(Ingredientes entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Ingredientes>>(
                "Ingredientes/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Ingredientes> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Ingredientes>>(
                "Ingredientes/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

        public Ingredientes Insertar(Ingredientes entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Ingredientes>(
                "Ingredientes/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

        private bool Validar(Ingredientes entidad)
        {
            return string.IsNullOrEmpty(entidad.Nombre) ||
                string.IsNullOrEmpty(entidad.UniMedida) ||
                entidad.CostoUni != 0;
        }
    }
}

