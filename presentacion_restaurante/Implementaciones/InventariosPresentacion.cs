
using System.Collections.Generic;
using System.Text;
using presentacion_restaurante.Interfaces;
using lib_aplicaciones.Entidades;

namespace presentacion_restaurante.Implementaciones
{
    public class InventariosPresentacion : IInventariosPresentacion
    {

        private IComunicaciones IComunicaciones;

        public InventariosPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public Inventarios Actualizar(Inventarios entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Inventarios>(
                "Inventarios/Actualizar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public Inventarios Borrar(Inventarios entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Inventarios>(
                "Inventarios/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Inventarios> Buscar(Inventarios entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Inventarios>>(
                "Inventarios/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Inventarios> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Inventarios>>(
                "Inventarios/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

        public Inventarios Insertar(Inventarios entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Inventarios>(
                "Inventarios/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

        private bool Validar(Inventarios entidad)
        {
            return 
                entidad.CantDisponible != 0||
                entidad.UltimaActu == DateTime.MinValue||
                entidad.CantMinima == 0;
        }
    }
}

