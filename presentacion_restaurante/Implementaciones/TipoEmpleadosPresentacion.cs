
using System.Collections.Generic;
using System.Text;
using presentacion_restaurante.Interfaces;
using lib_aplicaciones.Entidades;

namespace presentacion_restaurante.Implementaciones
{
    public class TipoEmpleadosPresentacion : ITipoEmpleadosPresentacion
    {

        private IComunicaciones IComunicaciones;

        public TipoEmpleadosPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public TipoEmpleados Actualizar(TipoEmpleados entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<TipoEmpleados>(
                "TipoEmpleados/Actualizar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public TipoEmpleados Borrar(TipoEmpleados entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<TipoEmpleados>(
                "TipoEmpleados/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<TipoEmpleados> Buscar(TipoEmpleados entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<TipoEmpleados>>(
                "TipoEmpleados/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<TipoEmpleados> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<TipoEmpleados>>(
                "TipoEmpleados/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

        public TipoEmpleados Insertar(TipoEmpleados entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<TipoEmpleados>(
                "TipoEmpleados/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

        private bool Validar(TipoEmpleados entidad)
        {
            return string.IsNullOrEmpty(entidad.Nombre) ||
                entidad.SalarioBase != 0;
        }
    }
}

