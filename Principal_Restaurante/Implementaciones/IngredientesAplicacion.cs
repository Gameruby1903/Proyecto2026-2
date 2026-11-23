using lib_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;
using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.Implementaciones
{
    public class IngredientesAplicacion : IIngredientesAplicacion
    {
        private IConexion IConexion;

        public IngredientesAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Ingredientes Actualizar(Ingredientes entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (!Validar(entidad))
                throw new Exception("Falta informacion");

            var entry = this.IConexion!.Entry<Ingredientes>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public Ingredientes Borrar(Ingredientes entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            // No borrar si otras tablas lo usan (si no, SQL lanza error de llave foránea)
            if (this.IConexion.RecetaIngredientes!.Any(x => x.Ingrediente == entidad.Id) ||
                this.IConexion.Inventarios!.Any(x => x.Ingrediente == entidad.Id) ||
                this.IConexion.DetalleCompras!.Any(x => x.Ingrediente == entidad.Id))
                throw new Exception("No se puede borrar el ingrediente porque tiene registros relacionados en: RecetaIngredientes, Inventarios, DetalleCompras");

            this.IConexion.Ingredientes!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Ingredientes> Buscar(Ingredientes entidad)
        {
            return this.IConexion.Ingredientes!
                .Include(x => x._Proveedor)
                .Where(x => string.IsNullOrEmpty(entidad.Nombre) ||
                            (x.Nombre != null && x.Nombre.Contains(entidad.Nombre)))
                .ToList();
        }
        public List<Ingredientes> Consultar()
        {
            return this.IConexion.Ingredientes!.Include(x => x._Proveedor).ToList();
        }

        public Ingredientes Insertar(Ingredientes entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (!Validar(entidad))
                throw new Exception("Falta informacion");

            this.IConexion.Ingredientes!.Add(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        private bool Validar(Ingredientes entidad)
        {
            if (string.IsNullOrEmpty(entidad.Nombre) || string.IsNullOrEmpty(entidad.UniMedida) || entidad.CostoUni == 0 || entidad.Proveedor == 0)
                return false;
            return true;
        }
    }
}
