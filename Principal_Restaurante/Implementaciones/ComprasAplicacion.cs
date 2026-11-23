using lib_aplicaciones.Entidades;
using lib_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class ComprasAplicacion : IComprasAplicacion
    {
        private IConexion IConexion;

        public ComprasAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Compras Actualizar(Compras entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (!Validar(entidad))
                throw new Exception("Falta informacion");

            var entry = this.IConexion!.Entry<Compras>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public Compras Borrar(Compras entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            // No borrar si otras tablas lo usan (si no, SQL lanza error de llave foránea)
            if (this.IConexion.DetalleCompras!.Any(x => x.Compra == entidad.Id))
                throw new Exception("No se puede borrar la compra porque tiene registros relacionados en: DetalleCompras");

            this.IConexion.Compras!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Compras> Buscar(Compras entidad)
        {
            return this.IConexion.Compras!.Include(x => x._Proveedor)
                .Where(x => x.Estado!.Contains(entidad.Estado!))
                .ToList();
        }

        public List<Compras> Consultar()
        {
            return this.IConexion.Compras!
                .Include(x => x._Proveedor).ToList();
        }

        public Compras Insertar(Compras entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (!Validar(entidad))
                throw new Exception("Falta informacion");

            if (entidad.Fecha == DateTime.MinValue)
                entidad.Fecha = DateTime.Now;

            this.IConexion.Compras!.Add(entidad!);
            this.IConexion.SaveChanges();
            return entidad;

        }

        private bool Validar(Compras entidad)
        {
            if (string.IsNullOrEmpty(entidad.Estado) ||
                entidad.Proveedor == 0 ||
                entidad.Fecha == DateTime.MinValue ||
                entidad.Total == 0)
                return false;
            return true;
        }
    }
}
