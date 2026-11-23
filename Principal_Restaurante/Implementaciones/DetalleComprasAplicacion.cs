
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

            // CORRECCIÓN: Se agrega ! porque Validar devuelve false
            // cuando la cantidad es menor o igual a cero.
            if (!Validar(entidad))
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
                .Include(x => x._Ingrediente)
                .Include(x => x._Compra)
                .Where(x => entidad.Cantidad == 0 || x.Cantidad == entidad.Cantidad)
                .ToList();
        }

        public List<DetalleCompras> Consultar()
        {
            return this.IConexion.DetalleCompras!.Include(x => x._Ingrediente).Include(x => x._Compra).ToList();
        }

        public DetalleCompras Insertar(DetalleCompras entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            // CORRECCIÓN: Se invierte la condición para lanzar
            // la excepción cuando los datos NO son válidos.
            if (!Validar(entidad))
                throw new Exception("Falta informacion");

            this.IConexion.DetalleCompras!.Add(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        private bool Validar(DetalleCompras entidad)
        {
            // La cantidad debe ser mayor que cero.
            // true = información válida.
            // false = información inválida.
            if (entidad.Cantidad <= 0 || entidad.PrecioUni <= 0 || entidad.Ingrediente == 0 || entidad.Compra == 0)
                return false;

            return true;
        }
    }
}

