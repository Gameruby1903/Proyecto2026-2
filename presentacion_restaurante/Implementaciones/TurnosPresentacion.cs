
using System.Collections.Generic;
using System.Text;
using presentacion_restaurante.Interfaces;
using lib_aplicaciones.Entidades;

namespace presentacion_restaurante.Implementaciones
{
    public class TurnosPresentacion : ITurnosPresentacion
    {

        private IComunicaciones IComunicaciones;

        public TurnosPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public Turnos Actualizar(Turnos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Turnos>(
                "Turnos/Actualizar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public Turnos Borrar(Turnos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Turnos>(
                "Turnos/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Turnos> Buscar(Turnos entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Turnos>>(
                "Turnos/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Turnos> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Turnos>>(
                "Turnos/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

        public Turnos Insertar(Turnos entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Turnos>(
                "Turnos/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

        private bool Validar(Turnos entidad)
        {
            return string.IsNullOrEmpty(entidad.Nombre) ||
                entidad.FechaInicio == DateTime.MinValue ||
                entidad.FechaFin == DateTime.MinValue;
        }
    }
}

