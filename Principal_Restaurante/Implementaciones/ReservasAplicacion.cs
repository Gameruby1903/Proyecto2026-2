using lib_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;
using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.Implementaciones
{
    public class ReservasAplicacion : IReservasAplicacion
    {
        private IConexion IConexion;

        public ReservasAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Reservas Actualizar(Reservas entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var entry = this.IConexion!.Entry<Reservas>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public Reservas Borrar(Reservas entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.Reservas!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Reservas> Buscar(Reservas entidad)
        {
            return this.IConexion.Reservas!
                .Where(x => x.Id == entidad.Id)
                .ToList();
        }

        public List<Reservas> Consultar()
        {
            return this.IConexion.Reservas!.ToList();
        }

        public Reservas Insertar(Reservas entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            this.IConexion.Reservas!.Add(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        private bool Validar(Reservas entidad)
        {
            if (entidad.Id <= 0)
                return false;

            return true;
        }
    }
}
