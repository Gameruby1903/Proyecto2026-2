
using System.Collections.Generic;
using System.Text;
using presentacion_restaurante.Interfaces;
using lib_aplicaciones.Entidades;

namespace presentacion_restaurante.Implementaciones
{
    public class ReservasPresentacion : IReservasPresentacion
    {

        private IComunicaciones IComunicaciones;

        public ReservasPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public Reservas Actualizar(Reservas entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Reservas>(
                "Reservas/Actualizar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public Reservas Borrar(Reservas entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Reservas>(
                "Reservas/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Reservas> Buscar(Reservas entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Reservas>>(
                "Reservas/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Reservas> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Reservas>>(
                "Reservas/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

        public Reservas Insertar(Reservas entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Reservas>(
                "Reservas/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

        private bool Validar(Reservas entidad)
        {
            return string.IsNullOrEmpty(entidad.Estado) ||
                entidad.FechaHora == DateTime.MinValue ||
                entidad.NPersonas != 0;
        }
    }
}

