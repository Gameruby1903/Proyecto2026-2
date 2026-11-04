
using System.Collections.Generic;
using System.Text;
using presentacion_restaurante.Interfaces;
using lib_aplicaciones.Entidades;

namespace presentacion_restaurante.Implementaciones
{
    public class DetallePedidosPresentacion : IDetallePedidosPresentacion
    {

        private IComunicaciones IComunicaciones;

        public DetallePedidosPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public DetallePedidos Actualizar(DetallePedidos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<DetallePedidos>(
                "DetallePedidos/Actualizar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public DetallePedidos Borrar(DetallePedidos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<DetallePedidos>(
                "DetallePedidos/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<DetallePedidos> Buscar(DetallePedidos entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<DetallePedidos>>(
                "DetallePedidos/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<DetallePedidos> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<DetallePedidos>>(
                "DetallePedidos/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

        public DetallePedidos Insertar(DetallePedidos entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<DetallePedidos>(
                "DetallePedidos/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

        private bool Validar(DetallePedidos entidad)
        {
            return entidad.PrecioUni != 0 ||
                entidad.Cantidad != 0;
        }
    }
}

