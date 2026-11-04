
using System.Collections.Generic;
using System.Text;
using presentacion_restaurante.Interfaces;
using lib_aplicaciones.Entidades;

namespace presentacion_restaurante.Implementaciones
{
    public class SucursalesPresentacion : ISucursalesPresentacion
    {

        private IComunicaciones IComunicaciones;

        public SucursalesPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public Sucursales Actualizar(Sucursales entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Sucursales>(
                "Sucursales/Actualizar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public Sucursales Borrar(Sucursales entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Sucursales>(
                "Sucursales/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Sucursales> Buscar(Sucursales entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Sucursales>>(
                "Sucursales/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Sucursales> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Sucursales>>(
                "Sucursales/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

        public Sucursales Insertar(Sucursales entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Sucursales>(
                "Sucursales/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

        private bool Validar(Sucursales entidad)
        {
            return string.IsNullOrEmpty(entidad.Nombre) ||
                string.IsNullOrEmpty(entidad.Direccion) ||
                string.IsNullOrEmpty(entidad.Telefono) ||
                string.IsNullOrEmpty(entidad.Ciudad);
        }
    }
}

