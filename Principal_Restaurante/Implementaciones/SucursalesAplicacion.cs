using lib_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;
using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.Implementaciones
{
    public class SucursalesAplicacion : ISucursalesAplicacion
    {
        private IConexion IConexion;

        public SucursalesAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Sucursales Actualizar(Sucursales entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var entry = this.IConexion!.Entry<Sucursales>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public Sucursales Borrar(Sucursales entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.Sucursales!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Sucursales> Buscar(Sucursales entidad)
        {
            return this.IConexion.Sucursales!
                .Where(x => x.Id == entidad.Id)
                .ToList();
        }

        public List<Sucursales> Consultar()
        {
            return this.IConexion.Sucursales!.ToList();
        }

        public Sucursales Insertar(Sucursales entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            this.IConexion.Sucursales!.Add(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        private bool Validar(Sucursales entidad)
        {
            if (entidad.Id <= 0)
                return false;

            return true;
        }
    }
}
