
using System.Collections.Generic;
using System.Text;
using presentacion_restaurante.Interfaces;
using lib_aplicaciones.Entidades;

namespace presentacion_restaurante.Implementaciones
{
    public class DetalleComprasPresentacion : IDetalleComprasPresentacion
    {
        private IComunicaciones IComunicaciones;

        public DetalleComprasPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public DetalleCompras Actualizar(DetalleCompras entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            // CORRECCIÓN: Se utiliza ! para lanzar la excepción
            // cuando la información no sea válida.
            if (!Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<DetalleCompras>(
                "DetalleCompras/Actualizar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public DetalleCompras Borrar(DetalleCompras entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<DetalleCompras>(
                "DetalleCompras/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<DetalleCompras> Buscar(DetalleCompras entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<DetalleCompras>>(
                "DetalleCompras/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<DetalleCompras> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<DetalleCompras>>(
                "DetalleCompras/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

        public DetalleCompras Insertar(DetalleCompras entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            // CORRECCIÓN: Se invierte la condición.
            if (!Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<DetalleCompras>(
                "DetalleCompras/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

        private bool Validar(DetalleCompras entidad)
        {
            // CORRECCIÓN: Los dos campos deben ser mayores que cero.
            if (entidad.PrecioUni <= 0 || entidad.Cantidad <= 0)
                return false;

            return true;
        }
    }
}
