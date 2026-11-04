
using System.Collections.Generic;
using System.Text;
using presentacion_restaurante.Interfaces;
using lib_aplicaciones.Entidades;

namespace presentacion_restaurante.Implementaciones
{
    public class MetodoPagosPresentacion : IMetodoPagosPresentacion
    {

        private IComunicaciones IComunicaciones;

        public MetodoPagosPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public MetodoPagos Actualizar(MetodoPagos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<MetodoPagos>(
                "MetodoPagos/Actualizar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public MetodoPagos Borrar(MetodoPagos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<MetodoPagos>(
                "MetodoPagos/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<MetodoPagos> Buscar(MetodoPagos entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<MetodoPagos>>(
                "MetodoPagos/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<MetodoPagos> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<MetodoPagos>>(
                "MetodoPagos/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

        public MetodoPagos Insertar(MetodoPagos entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<MetodoPagos>(
                "MetodoPagos/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

        private bool Validar(MetodoPagos entidad)
        {
            return string.IsNullOrEmpty(entidad.Tipo) ||
                string.IsNullOrEmpty(entidad.Descripcion);
                
        }
    }
}

