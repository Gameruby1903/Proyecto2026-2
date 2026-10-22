using lib_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;
using lib_aplicaciones.Entidades;
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

            if (Validar(entidad))
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

            this.IConexion.Compras!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Compras> Buscar(Compras entidad)
        {
            return this.IConexion.Compras!
                .Where(x => x.Estado!.Contains(entidad.Estado!))
                .ToList();
        }

        public List<Compras> Consultar()
        {
            return this.IConexion.Compras!.ToList();
        }

        public Compras Insertar(Compras entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            this.IConexion.Compras!.Add(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        private bool Validar(Compras entidad)
        {
            if (string.IsNullOrEmpty(entidad.Estado))
                return false;
            return true;
        }
    }
}
