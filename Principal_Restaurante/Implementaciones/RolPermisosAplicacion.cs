using lib_aplicaciones.Entidades;
using lib_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class RolPermisosAplicacion : IRolPermisosAplicacion
    {
        private IConexion IConexion;

        public RolPermisosAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public RolPermisos Insertar(RolPermisos entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("La asignación ya está guardada");

            if (entidad.Rol <= 0 || entidad.Permiso <= 0)
                throw new Exception("Seleccione un rol y un permiso");

            var rol = this.IConexion.Roles!
                .FirstOrDefault(x => x.Id == entidad.Rol);

            if (rol == null)
                throw new Exception("El rol no existe");

            var permiso = this.IConexion.Permisos!
                .FirstOrDefault(x => x.Id == entidad.Permiso);

            if (permiso == null)
                throw new Exception("El permiso no existe");

            if (this.IConexion.RolPermisos!.Any(x =>
                x.Rol == entidad.Rol &&
                x.Permiso == entidad.Permiso))
                throw new Exception("El rol ya tiene asignado ese permiso");

            this.IConexion.RolPermisos!.Add(entidad);
            this.IConexion.Entry(entidad).State = EntityState.Added;
            this.IConexion.SaveChanges();

            return entidad;
        }

        public RolPermisos Actualizar(RolPermisos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha seleccionado una asignación");

            if (entidad.Rol <= 0 || entidad.Permiso <= 0)
                throw new Exception("Seleccione un rol y un permiso");

            var actual = this.IConexion.RolPermisos!
                .FirstOrDefault(x => x.Id == entidad.Id);

            if (actual == null)
                throw new Exception("La asignación no existe");

            if (!this.IConexion.Roles!.Any(x => x.Id == entidad.Rol))
                throw new Exception("El rol no existe");

            if (!this.IConexion.Permisos!.Any(x => x.Id == entidad.Permiso))
                throw new Exception("El permiso no existe");

            if (this.IConexion.RolPermisos!.Any(x =>
                x.Id != entidad.Id &&
                x.Rol == entidad.Rol &&
                x.Permiso == entidad.Permiso))
                throw new Exception("El rol ya tiene asignado ese permiso");

            actual.Rol = entidad.Rol;
            actual.Permiso = entidad.Permiso;

            this.IConexion.Entry(actual).State = EntityState.Modified;
            this.IConexion.SaveChanges();

            return actual;
        }

        public RolPermisos Borrar(RolPermisos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha seleccionado una asignación");

            var actual = this.IConexion.RolPermisos!
                .FirstOrDefault(x => x.Id == entidad.Id);

            if (actual == null)
                throw new Exception("La asignación no existe");

            this.IConexion.RolPermisos!.Remove(actual);
            this.IConexion.SaveChanges();

            return actual;
        }

        public List<RolPermisos> Consultar()
        {
            return this.IConexion.RolPermisos!.ToList();
        }

        public List<RolPermisos> Buscar(RolPermisos entidad)
        {
            var consulta = this.IConexion.RolPermisos!.AsQueryable();

            if (entidad.Rol > 0)
            {
                consulta = consulta.Where(x => x.Rol == entidad.Rol);
            }

            if (entidad.Permiso > 0)
            {
                consulta = consulta.Where(x => x.Permiso == entidad.Permiso);
            }

            return consulta.ToList();
        }

        public List<RolPermisos> ConsultarPorRol(RolPermisos entidad)
        {
            if (entidad.Rol <= 0)
                throw new Exception("Seleccione un rol");

            return this.IConexion.RolPermisos!
                .Where(x => x.Rol == entidad.Rol)
                .ToList();
        }
    }
}