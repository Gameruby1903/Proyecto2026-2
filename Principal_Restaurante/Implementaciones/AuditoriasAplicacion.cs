using lib_aplicaciones.Entidades;
using lib_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class AuditoriasAplicacion : IAuditoriasAplicacion
    {
        private IConexion IConexion;

        public AuditoriasAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }
        public List<Auditorias> Consultar()
        {
            return this.IConexion.Auditorias!
                .OrderByDescending(x => x.Id)
                .Take(500)
                .ToList();
        }

        public List<Auditorias> Buscar(Auditorias entidad)
        {
            var consulta = this.IConexion.Auditorias!.AsQueryable();

            if (!string.IsNullOrEmpty(entidad.Usuario))
                consulta = consulta.Where(x => x.Usuario!.Contains(entidad.Usuario));

            if (!string.IsNullOrEmpty(entidad.Entidad))
                consulta = consulta.Where(x => x.Entidad!.Contains(entidad.Entidad));

            return consulta.OrderByDescending(x => x.Id).Take(500).ToList();
        }
    }
}


