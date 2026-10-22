using lib_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;
using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.Implementaciones
{
    public class ProductossAplicacion : IProductossAplicacion
    {
        private IConexion IConexion;

        public ProductossAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Productoss Actualizar(Productoss entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var entry = this.IConexion!.Entry<Productoss>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public Productoss Borrar(Productoss entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.Productoss!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Productoss> Buscar(Productoss entidad)
        {
            return this.IConexion.Productoss!
                .Where(x => x.Id == entidad.Id)
                .ToList();
        }

        public List<Productoss> Consultar()
        {
            return this.IConexion.Productoss!.ToList();
        }

        public Productoss Insertar(Productoss entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            this.IConexion.Productoss!.Add(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        private bool Validar(Productoss entidad)
        {
            if (entidad.Id <= 0)
                return false;

            return true;
        }
    }
}
