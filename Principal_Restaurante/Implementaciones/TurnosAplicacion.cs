using lib_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;
using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.Implementaciones
{
    public class TurnosAplicacion : ITurnosAplicacion
    {
        private IConexion IConexion;

        public TurnosAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Turnos Actualizar(Turnos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (!Validar(entidad))
                throw new Exception("Falta informacion");

            var entry = this.IConexion!.Entry<Turnos>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public Turnos Borrar(Turnos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.Turnos!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }
        public List<Turnos> Consultar(Turnos entidad)
        {
            return this.IConexion.Turnos!
                .Include(x => x._Empleado)
                .Where(x => string.IsNullOrEmpty(entidad.Nombre) ||
                            (x.Nombre != null && x.Nombre.Contains(entidad.Nombre)))
                .ToList();
        }
        public List<Turnos> Consultar()
        {
            return this.IConexion.Turnos!.Include(x => x._Empleado).ToList();
        }

        public Turnos Insertar(Turnos entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (!Validar(entidad))
                throw new Exception("Falta informacion");

            this.IConexion.Turnos!.Add(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        private bool Validar(Turnos entidad)
        {
            if (string.IsNullOrEmpty(entidad.Nombre) || entidad.Empleado == 0 || entidad.FechaInicio == DateTime.MinValue
                || entidad.FechaFin == DateTime.MinValue)
                return false;

            return true;
        }
    }
}
