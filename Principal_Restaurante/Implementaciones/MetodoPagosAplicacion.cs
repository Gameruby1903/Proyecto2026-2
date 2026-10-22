using lib_aplicaciones.Entidades;
using lib_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class MetodoPagosAplicacion : IMetodoPagosAplicacion
    {
        private IConexion IConexion;

        public MetodoPagosAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public MetodoPagos Actualizar(MetodoPagos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var entry = this.IConexion!.Entry<MetodoPagos>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public MetodoPagos Borrar(MetodoPagos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.MetodoPagos!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<MetodoPagos> Buscar(MetodoPagos entidad)
        {
            return this.IConexion.MetodoPagos!
                .Where(x => x.Tipo!.Contains(entidad.Tipo!))
                .ToList();
        }

        public List<MetodoPagos> Consultar()
        {
            return this.IConexion.MetodoPagos!.ToList();
        }

        public MetodoPagos Insertar(MetodoPagos entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            this.IConexion.MetodoPagos!.Add(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        private bool Validar(MetodoPagos entidad)
        {
            if (string.IsNullOrEmpty(entidad.Tipo))
                return false;
            return true;
        }
    }
}
