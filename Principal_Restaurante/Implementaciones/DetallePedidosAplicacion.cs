using lib_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;
using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.Implementaciones
{
    public class DetallePedidosAplicacion : IDetallePedidosAplicacion
    {
        private IConexion IConexion;

        public DetallePedidosAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public DetallePedidos Actualizar(DetallePedidos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (!Validar(entidad))
                throw new Exception("Falta informacion");

            if (!this.IConexion.Pedidos!.Any(x => x.Id == entidad.Pedido))
                throw new Exception("El pedido no existe");

            if (!this.IConexion.Productos!.Any(x => x.Id == entidad.Producto))
                throw new Exception("El producto no existe");

            var entry = this.IConexion!.Entry<DetallePedidos>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public DetallePedidos Borrar(DetallePedidos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.DetallePedidos!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<DetallePedidos> Buscar(DetallePedidos entidad)
        {
            return this.IConexion.DetallePedidos!
                .Include(x => x._Pedido)
                .Include(x => x._Producto)
                .Where(x => entidad.Cantidad == 0 || x.Cantidad == entidad.Cantidad)
                .ToList();
        }

        public List<DetallePedidos> Consultar()
        {
            return this.IConexion.DetallePedidos!.Include(x => x._Pedido).Include(x => x._Producto).ToList();
        }

        public DetallePedidos Insertar(DetallePedidos entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (!Validar(entidad))
                throw new Exception("Falta informacion");

            if (!this.IConexion.Pedidos!.Any(x => x.Id == entidad.Pedido))
                throw new Exception("El pedido no existe");

            if (!this.IConexion.Productos!.Any(x => x.Id == entidad.Producto))
                throw new Exception("El producto no existe");

            this.IConexion.DetallePedidos!.Add(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        private bool Validar(DetallePedidos entidad)
        {
            if (entidad.Cantidad <= 0 || entidad.PrecioUni <= 0 || entidad.Pedido == 0 || entidad.Producto == 0)
                return false;
            return true;
        }
    }
}
