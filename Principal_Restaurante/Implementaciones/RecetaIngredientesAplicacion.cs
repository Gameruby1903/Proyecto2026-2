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

            if (Validar(entidad))
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
            return this.IConexion.RecetaIngredientes!
                .Where(x => x.Id == entidad.Id)
                .ToList();
        }

        public List<RecetaIngredientes> Consultar()
        {
            return this.IConexion.RecetaIngredientes!.ToList();
        }

        public RecetaIngredientes Insertar(RecetaIngredientes entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            this.IConexion.RecetaIngredientes!.Add(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        private bool Validar(RecetaIngredientes entidad)
        {
            if (entidad.Id <= 0)
                return false;

            return true;
        }
    }
}
