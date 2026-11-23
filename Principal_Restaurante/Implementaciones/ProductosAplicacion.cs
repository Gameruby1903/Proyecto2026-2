using lib_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;
using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.Implementaciones
{
    public class ProductosAplicacion : IProductosAplicacion
    {
        private IConexion IConexion;

        public ProductosAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Productos Actualizar(Productos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (!Validar(entidad))
                throw new Exception("Falta informacion");

            var entry = this.IConexion!.Entry<Productos>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public Productos Borrar(Productos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            // No borrar si otras tablas lo usan (si no, SQL lanza error de llave foránea)
            if (this.IConexion.DetallePedidos!.Any(x => x.Producto == entidad.Id) ||
                this.IConexion.RecetaIngredientes!.Any(x => x.Producto == entidad.Id) ||
                this.IConexion.Resenas!.Any(x => x.Producto == entidad.Id))
                throw new Exception("No se puede borrar el producto porque tiene registros relacionados en: DetallePedidos, RecetaIngredientes, Resenas");

            this.IConexion.Productos!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Productos> Buscar(Productos entidad)
        {
            return this.IConexion.Productos!
                .Include(x => x._Categoria)
                .Where(x => string.IsNullOrEmpty(entidad.Nombre) ||
                            (x.Nombre != null && x.Nombre.Contains(entidad.Nombre)))
                .ToList();
        }

        public List<Productos> Consultar()
        {
            return this.IConexion.Productos!.Include(x => x._Categoria).ToList();
        }

        public Productos Insertar(Productos entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (!Validar(entidad))
                throw new Exception("Falta informacion");

            this.IConexion.Productos!.Add(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        private bool Validar(Productos entidad)
        {

            if (string.IsNullOrEmpty(entidad.Nombre) || entidad.Categoria == 0 || entidad.Precio <= 0 || 
                string.IsNullOrEmpty(entidad.Descripcion) || entidad.Disponible == false)

         
                return false;
            return true;
        }
    }
}
