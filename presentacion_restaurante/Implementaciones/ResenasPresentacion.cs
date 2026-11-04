
using System.Collections.Generic;
using System.Text;
using presentacion_restaurante.Interfaces;
using lib_aplicaciones.Entidades;

namespace presentacion_restaurante.Implementaciones
{
    public class ResenasPresentacion : IResenasPresentacion
    {

        private IComunicaciones IComunicaciones;

        public ResenasPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public Resenas Actualizar(Resenas entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Resenas>(
                "Resenas/Actualizar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public Resenas Borrar(Resenas entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Resenas>(
                "Resenas/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Resenas> Buscar(Resenas entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Resenas>>(
                "Resenas/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Resenas> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Resenas>>(
                "Resenas/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

        public Resenas Insertar(Resenas entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Resenas>(
                "Resenas/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

        private bool Validar(Resenas entidad)
        {
            return string.IsNullOrEmpty(entidad.Comentario) ||

                entidad.Calificacion != 0 ||
            entidad.Fecha == DateTime.MinValue;
        }
    }
}

