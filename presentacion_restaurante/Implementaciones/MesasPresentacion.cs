
using System.Collections.Generic;
using System.Text;
using presentacion_restaurante.Interfaces;
using lib_aplicaciones.Entidades;

namespace presentacion_restaurante.Implementaciones
{
    public class MesasPresentacion : IMesasPresentacion
    {

        private IComunicaciones IComunicaciones;

        public MesasPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public Mesas Actualizar(Mesas entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Mesas>(
                "Mesas/Actualizar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public Mesas Borrar(Mesas entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Mesas>(
                "Mesas/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Mesas> Buscar(Mesas entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Mesas>>(
                "Mesas/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Mesas> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Mesas>>(
                "Mesas/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

        public Mesas Insertar(Mesas entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Mesas>(
                "Mesas/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

        private bool Validar(Mesas entidad)
        {
            return
                string.IsNullOrEmpty(entidad.Ubicacion) ||
                entidad.Capacidad == 0||
                entidad.Estado == true ||
                entidad.Numero != 0;
        }
    }
}

