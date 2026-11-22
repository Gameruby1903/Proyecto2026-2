using lib_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;
using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.Implementaciones
{
    public class ProveedoresAplicacion : IProveedoresAplicacion
    {
        private IConexion IConexion;

        public ProveedoresAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Proveedores Actualizar(Proveedores entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (!Validar(entidad))
                throw new Exception("Falta informacion");

            var entry = this.IConexion!.Entry<Proveedores>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public Proveedores Borrar(Proveedores entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.Proveedores!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Proveedores> Buscar(Proveedores entidad)
        {
            return this.IConexion.Proveedores!
                .Where(x => x.Nombre!.Contains(entidad.Nombre!))
                .ToList();
        }

        public List<Proveedores> Consultar()
        {
            return this.IConexion.Proveedores!.ToList();
        }

        public Proveedores Insertar(Proveedores entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (!Validar(entidad))
                throw new Exception("Falta informacion");

            this.IConexion.Proveedores!.Add(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        private bool Validar(Proveedores entidad)
        {
            if (string.IsNullOrEmpty(entidad.Nombre) ||
                string.IsNullOrEmpty(entidad.Telefono) || 
                string.IsNullOrEmpty(entidad.Email) || string.IsNullOrEmpty(entidad.Direccion))
                return false;

            return true;
        }
    }
}
