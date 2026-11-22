using lib_aplicaciones.Entidades;
using lib_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class RolesAplicacion : IRolesAplicacion
    {
        private IConexion IConexion;

        public RolesAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Roles Insertar(Roles entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("El rol ya está guardado");

            if (!Validar(entidad))
                throw new Exception("Falta informacion");

            if (this.IConexion.Roles!.Any(x =>
                x.Nombre == entidad.Nombre))
                throw new Exception("El rol ya existe");

            this.IConexion.Roles!.Add(entidad);
            this.IConexion.Entry(entidad).State = EntityState.Added;
            this.IConexion.SaveChanges();

            return entidad;
        }

        public Roles Actualizar(Roles entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha seleccionado un rol");

            if (!Validar(entidad))
                throw new Exception("Falta informacion");

            var actual = this.IConexion.Roles!
                .FirstOrDefault(x => x.Id == entidad.Id);

            if (actual == null)
                throw new Exception("El rol no existe");

            if (this.IConexion.Roles!.Any(x =>
                x.Id != entidad.Id &&
                x.Nombre == entidad.Nombre))
                throw new Exception("El nombre del rol ya existe");

            actual.Nombre = entidad.Nombre;
            actual.Descripcion = entidad.Descripcion;

            this.IConexion.Entry(actual).State = EntityState.Modified;
            this.IConexion.SaveChanges();

            return actual;
        }

        public Roles Borrar(Roles entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha seleccionado un rol");

            var actual = this.IConexion.Roles!
                .FirstOrDefault(x => x.Id == entidad.Id);

            if (actual == null)
                throw new Exception("El rol no existe");

            if (this.IConexion.Usuarios!.Any(x => x.Rol == entidad.Id))
                throw new Exception("No se puede borrar el rol porque tiene usuarios asociados");

            if (this.IConexion.RolPermisos!.Any(x => x.Rol == entidad.Id))
                throw new Exception("Primero debe eliminar los permisos asociados al rol");

            this.IConexion.Roles!.Remove(actual);
            this.IConexion.SaveChanges();

            return actual;
        }

        public List<Roles> Consultar()
        {
            return this.IConexion.Roles!.ToList();
        }

        public List<Roles> Buscar(Roles entidad)
        {
            var consulta = this.IConexion.Roles!.AsQueryable();

            if (!string.IsNullOrEmpty(entidad.Nombre))
            {
                consulta = consulta.Where(x =>
                    x.Nombre!.Contains(entidad.Nombre));
            }

            if (!string.IsNullOrEmpty(entidad.Descripcion))
            {
                consulta = consulta.Where(x =>
                    x.Descripcion!.Contains(entidad.Descripcion));
            }

            return consulta.ToList();
        }

        private bool Validar(Roles entidad)
        {
            if (string.IsNullOrEmpty(entidad.Nombre))
                return true;

            return false;
        }
    }
}