using lib_aplicaciones.Entidades;
using lib_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class PermisosAplicacion : IPermisosAplicacion
    {
        private IConexion IConexion;

        public PermisosAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Permisos Insertar(Permisos entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("El permiso ya está guardado");

            if (!Validar(entidad))
                throw new Exception("Falta informacion");

            if (this.IConexion.Permisos!.Any(x =>
                x.Codigo == entidad.Codigo))
                throw new Exception("El código del permiso ya existe");

            this.IConexion.Permisos!.Add(entidad);
            this.IConexion.Entry(entidad).State = EntityState.Added;
            this.IConexion.SaveChanges();

            return entidad;
        }

        public Permisos Actualizar(Permisos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha seleccionado un permiso");

            if (!Validar(entidad))
                throw new Exception("Falta informacion");

            var actual = this.IConexion.Permisos!
                .FirstOrDefault(x => x.Id == entidad.Id);

            if (actual == null)
                throw new Exception("El permiso no existe");

            if (this.IConexion.Permisos!.Any(x =>
                x.Id != entidad.Id &&
                x.Codigo == entidad.Codigo))
                throw new Exception("El código del permiso ya existe");

            actual.Codigo = entidad.Codigo;
            actual.Descripcion = entidad.Descripcion;

            this.IConexion.Entry(actual).State = EntityState.Modified;
            this.IConexion.SaveChanges();

            return actual;
        }

        public Permisos Borrar(Permisos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha seleccionado un permiso");

            var actual = this.IConexion.Permisos!
                .FirstOrDefault(x => x.Id == entidad.Id);

            if (actual == null)
                throw new Exception("El permiso no existe");

            if (this.IConexion.RolPermisos!.Any(x =>
                x.Permiso == entidad.Id))
                throw new Exception("El permiso está asignado a uno o varios roles");

            this.IConexion.Permisos!.Remove(actual);
            this.IConexion.SaveChanges();

            return actual;
        }

        public List<Permisos> Consultar()
        {
            return this.IConexion.Permisos!.ToList();
        }

        public List<Permisos> Buscar(Permisos entidad)
        {
            var consulta = this.IConexion.Permisos!.AsQueryable();

            if (!string.IsNullOrEmpty(entidad.Codigo))
            {
                consulta = consulta.Where(x =>
                    x.Codigo!.Contains(entidad.Codigo));
            }

            if (!string.IsNullOrEmpty(entidad.Descripcion))
            {
                consulta = consulta.Where(x =>
                    x.Descripcion!.Contains(entidad.Descripcion));
            }

            return consulta.ToList();
        }

        private bool Validar(Permisos entidad)
        {
            if (string.IsNullOrEmpty(entidad.Codigo))
                return true;

            return false;
        }
    }
}