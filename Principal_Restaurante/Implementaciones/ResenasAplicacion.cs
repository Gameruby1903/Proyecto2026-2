using lib_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;
using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.Implementaciones
{
    public class ResenasAplicacion : IResenasAplicacion
    {
        private IConexion IConexion;

        public ResenasAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Resenas Actualizar(Resenas entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (!Validar(entidad))
                throw new Exception("Falta informacion");

            var entry = this.IConexion!.Entry<Resenas>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public Resenas Borrar(Resenas entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.Resenas!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Resenas> Buscar(Resenas entidad)
        {
            return this.IConexion.Resenas!.Include(x => x._Producto).Include(x => x._Cliente)
                .Where(x => x.Calificacion == entidad.Calificacion)
                .ToList();
        }

        public List<Resenas> Consultar()
        {
            return this.IConexion.Resenas!.Include(x => x._Producto).Include(x => x._Cliente).ToList();
        }

        public Resenas Insertar(Resenas entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (!Validar(entidad))
                throw new Exception("Falta informacion");

            this.IConexion.Resenas!.Add(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        private bool Validar(Resenas entidad)
        {
            if (entidad.Calificacion <= 0 || string.IsNullOrEmpty(entidad.Comentario) || entidad.Cliente == 0 || entidad.Producto == 0 
                || entidad.Fecha == DateTime.MinValue)
                return false;

            return true;
        }
    }
}
