using lib_aplicaciones.Entidades;
using lib_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class PedidosAplicacion : IPedidosAplicacion
    {
        private IConexion IConexion;

        public PedidosAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Pedidos Actualizar(Pedidos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (!Validar(entidad))
                throw new Exception("Falta informacion");

            var entry = this.IConexion!.Entry<Pedidos>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public Pedidos Borrar(Pedidos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.Pedidos!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Pedidos> Buscar(Pedidos entidad)
        {

            return this.IConexion.Pedidos!.Include(x => x._Mesa).Include(x => x._Cliente).Include(x => x._Empleado).ToList();
               

            

              
        }

        public List<Pedidos> Consultar()
        {
            return this.IConexion.Pedidos!.Include(x => x._Mesa).Include(x => x._Cliente).Include(x => x._Empleado).ToList();
        }

        public Pedidos Insertar(Pedidos entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");


            if (string.IsNullOrEmpty(entidad.Estado))
                entidad.Estado = "Pendiente";

            if (entidad.Fecha == DateTime.MinValue)
                entidad.Fecha = DateTime.Now;

            if (!Validar(entidad))
                throw new Exception("Falta informacion");

            this.IConexion.Pedidos!.Add(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        private bool Validar(Pedidos entidad)
        {

            if (entidad.Fecha == DateTime.MinValue || entidad.Mesa == 0 || entidad.Cliente == 0 || entidad.Empleado == 0 ||
                string.IsNullOrEmpty(entidad.Estado))

           
                return false;
            return true;
        }
    }
}
