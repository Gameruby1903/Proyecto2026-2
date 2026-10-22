using lib_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;
using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.Implementaciones
{
    public class DetalleComprasAplicacion : IDetalleComprasAplicacion
    {
        private IConexion IConexion;

        public DetalleComprasAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public DetalleCompras Actualizar(DetalleCompras entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var entry = this.IConexion!.Entry<DetalleCompras>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public DetalleCompras Borrar(DetalleCompras entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.DetalleCompras!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<DetalleCompras> Buscar(DetalleCompras entidad)
        {
            return this.IConexion.DetalleCompras!
                .Where(x => x.Cantidad == entidad.Cantidad)
                .ToList();
        }

        public List<DetalleCompras> Consultar()
        {
            return this.IConexion.DetalleCompras!.ToList();
        }

        public DetalleCompras Insertar(DetalleCompras entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            this.IConexion.DetalleCompras!.Add(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        private bool Validar(DetalleCompras entidad)
        {
            if (entidad.Cantidad <= 0)
                return false;

            return true;
        }
    }
}
