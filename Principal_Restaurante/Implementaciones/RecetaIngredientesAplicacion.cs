using lib_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;
using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.Implementaciones
{
    public class RecetaIngredientesAplicacion : IRecetaIngredientesAplicacion
    {
        private IConexion IConexion;

        public RecetaIngredientesAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public RecetaIngredientes Actualizar(RecetaIngredientes entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (!Validar(entidad))
                throw new Exception("Falta informacion");

            var entry = this.IConexion!.Entry<RecetaIngredientes>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public RecetaIngredientes Borrar(RecetaIngredientes entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.RecetaIngredientes!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<RecetaIngredientes> Buscar(RecetaIngredientes entidad)
        {
            return this.IConexion.RecetaIngredientes!.Include(x => x._Producto).Include(x => x._Ingrediente)
                .Where(x => x.CantiReque == entidad.CantiReque)
                .ToList();
        }

        public List<RecetaIngredientes> Consultar()
        {
            return this.IConexion.RecetaIngredientes!.Include(x => x._Producto).Include(x => x._Ingrediente).ToList();
        }

        public RecetaIngredientes Insertar(RecetaIngredientes entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (!Validar(entidad))
                throw new Exception("Falta informacion");

            this.IConexion.RecetaIngredientes!.Add(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        private bool Validar(RecetaIngredientes entidad)
        {
            if (entidad.Producto == 0 || entidad.Ingrediente == 0 || entidad.CantiReque <= 0 || string.IsNullOrEmpty(entidad.Notas))
                return false;

            return true;
        }
    }
}
