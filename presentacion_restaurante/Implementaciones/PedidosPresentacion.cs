
using System.Collections.Generic;
using System.Text;
using presentacion_restaurante.Interfaces;
using lib_aplicaciones.Entidades;

namespace presentacion_restaurante.Implementaciones
{
    public class PedidosPresentacion : IPedidosPresentacion
    {

        private IComunicaciones IComunicaciones;

        public PedidosPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public Pedidos Actualizar(Pedidos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Pedidos>(
                "Pedidos/Actualizar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public Pedidos Borrar(Pedidos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Pedidos>(
                "Pedidos/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Pedidos> Buscar(Pedidos entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Pedidos>>(
                "Pedidos/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Pedidos> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Pedidos>>(
                "Pedidos/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

        public Pedidos Insertar(Pedidos entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Pedidos>(
                "Pedidos/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

        private bool Validar(Pedidos entidad)
        {
            return 
                string.IsNullOrEmpty(entidad.Estado) ||
                entidad.Fecha == DateTime.MinValue;
            
        }
    }
}

