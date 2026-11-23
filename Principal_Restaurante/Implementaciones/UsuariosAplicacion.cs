using lib_aplicaciones.Entidades;
using lib_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class UsuariosAplicacion : IUsuariosAplicacion
    {
        private IConexion IConexion;

        public UsuariosAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Usuarios Insertar(Usuarios entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("El usuario ya está guardado");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            if (this.IConexion.Usuarios!.Any(x =>
                x.NombreUsuario == entidad.NombreUsuario))
                throw new Exception("El nombre de usuario ya existe");

            if (!this.IConexion.Roles!.Any(x => x.Id == entidad.Rol))
                throw new Exception("El rol no existe");

            if (entidad.Empleado.HasValue &&
                !this.IConexion.Empleados!.Any(x => x.Id == entidad.Empleado))
                throw new Exception("El empleado no existe");

            if (entidad.Cliente.HasValue &&
                !this.IConexion.Clientes!.Any(x => x.Id == entidad.Cliente))
                throw new Exception("El cliente no existe");

            this.IConexion.Usuarios!.Add(entidad);
            this.IConexion.Entry(entidad).State = EntityState.Added;
            this.IConexion.SaveChanges();

            return entidad;
        }

        public Usuarios Actualizar(Usuarios entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha seleccionado un usuario");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var actual = this.IConexion.Usuarios!
                .FirstOrDefault(x => x.Id == entidad.Id);

            if (actual == null)
                throw new Exception("El usuario no existe");

            if (this.IConexion.Usuarios!.Any(x =>
                x.Id != entidad.Id &&
                x.NombreUsuario == entidad.NombreUsuario))
                throw new Exception("El nombre de usuario ya existe");

            if (!this.IConexion.Roles!.Any(x => x.Id == entidad.Rol))
                throw new Exception("El rol no existe");

            if (entidad.Empleado.HasValue &&
                !this.IConexion.Empleados!.Any(x => x.Id == entidad.Empleado))
                throw new Exception("El empleado no existe");

            if (entidad.Cliente.HasValue &&
                !this.IConexion.Clientes!.Any(x => x.Id == entidad.Cliente))
                throw new Exception("El cliente no existe");

            actual.NombreUsuario = entidad.NombreUsuario;
            actual.Clave = entidad.Clave;
            actual.Activo = entidad.Activo;
            actual.Rol = entidad.Rol;
            actual.Empleado = entidad.Empleado;
            actual.Cliente = entidad.Cliente;

            this.IConexion.Entry(actual).State = EntityState.Modified;
            this.IConexion.SaveChanges();

            return actual;
        }

        public Usuarios Borrar(Usuarios entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha seleccionado un usuario");

            var actual = this.IConexion.Usuarios!
                .FirstOrDefault(x => x.Id == entidad.Id);

            if (actual == null)
                throw new Exception("El usuario no existe");

            this.IConexion.Usuarios!.Remove(actual);
            this.IConexion.SaveChanges();

            return actual;
        }

        public List<Usuarios> Consultar()
        {
            return this.IConexion.Usuarios!.ToList();
        }

        public List<Usuarios> Buscar(Usuarios entidad)
        {
            var consulta = this.IConexion.Usuarios!.AsQueryable();

            if (!string.IsNullOrEmpty(entidad.NombreUsuario))
            {
                consulta = consulta.Where(x =>
                    x.NombreUsuario!.Contains(entidad.NombreUsuario));
            }

            if (entidad.Rol > 0)
            {
                consulta = consulta.Where(x => x.Rol == entidad.Rol);
            }

            if (entidad.Empleado.HasValue)
            {
                consulta = consulta.Where(x =>
                    x.Empleado == entidad.Empleado);
            }

            if (entidad.Cliente.HasValue)
            {
                consulta = consulta.Where(x =>
                    x.Cliente == entidad.Cliente);
            }

            return consulta.ToList();
        }

        public Usuarios? IniciarSesion(Usuarios entidad)
        {
            if (string.IsNullOrEmpty(entidad.NombreUsuario) ||
                string.IsNullOrEmpty(entidad.Clave))
                return null;

            var usuario = this.IConexion.Usuarios!
                .FirstOrDefault(x =>
                    x.NombreUsuario == entidad.NombreUsuario &&
                    x.Clave == entidad.Clave &&
                    x.Activo == true);

            if (usuario == null)
                return null;

            usuario.UltimoAcceso = DateTime.Now;

            this.IConexion.Entry(usuario).State = EntityState.Modified;
            this.IConexion.SaveChanges();

            return usuario;
        }

        public List<Usuarios> ConsultarPorRol(Usuarios entidad)
        {
            if (entidad.Rol <= 0)
                throw new Exception("Seleccione un rol");

            return this.IConexion.Usuarios!
                .Where(x => x.Rol == entidad.Rol)
                .ToList();
        }
      
           public List<string> ConsultarPermisos(Usuarios entidad)
        {
            var consulta =
                from u in this.IConexion.Usuarios!
                join rp in this.IConexion.RolPermisos!
                    on u.Rol equals rp.Rol
                join p in this.IConexion.Permisos!
                    on rp.Permiso equals p.Id
                where u.Id == entidad.Id
                select p.Codigo;

            return consulta.ToList()!;
        
        }
        public List<string> ConsultarPermisosPorNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return new List<string>();

            var usuario = this.Consultar()
                .FirstOrDefault(u => string.Equals(
                    u.NombreUsuario, nombre, StringComparison.OrdinalIgnoreCase));

            if (usuario == null)
                return new List<string>();

            return this.ConsultarPermisos(new Usuarios { Id = usuario.Id });
        }

        private bool Validar(Usuarios entidad)
        {
            if (string.IsNullOrEmpty(entidad.NombreUsuario) ||
                string.IsNullOrEmpty(entidad.Clave) ||
                entidad.Rol <= 0)
                return true;

            // La base de datos exige empleado o cliente,
            // pero no permite que ambos tengan valor al mismo tiempo.
            if (entidad.Empleado.HasValue == entidad.Cliente.HasValue)
                return true;

            return false;
        }
    }
}