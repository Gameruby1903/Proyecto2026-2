using lib_aplicaciones.Entidades;
using lib_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace lib_aplicaciones.Implementaciones
{
    public class InventariosAplicacion : IInventariosAplicacion
    {
        private IConexion IConexion;

        public InventariosAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Inventarios Actualizar(Inventarios entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (!Validar(entidad))
                throw new Exception("Falta informacion");

            var entry = this.IConexion!.Entry<Inventarios>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public Inventarios Borrar(Inventarios entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.Inventarios!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Inventarios> Buscar(Inventarios entidad)
        {
            return this.IConexion.Inventarios!.Include(x => x._Ingrediente)
                .Where(x => x.CantDisponible == entidad.CantDisponible)
                .ToList();
        }

        public List<Inventarios> Consultar()
        {
            return this.IConexion.Inventarios!.Include(x => x._Ingrediente).ToList();
        }

        public Inventarios Insertar(Inventarios entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (!Validar(entidad))
                throw new Exception("Falta informacion");

            this.IConexion.Inventarios!.Add(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        private bool Validar(Inventarios entidad)
        {
            if (entidad.CantDisponible <= 0 || entidad.Ingrediente == 0 || entidad.CantDisponible <= 0 || 
                entidad.CantMinima <= 0 || entidad.UltimaActu == DateTime.MinValue)
            
                return false;

            return true;
        }
    }
}
