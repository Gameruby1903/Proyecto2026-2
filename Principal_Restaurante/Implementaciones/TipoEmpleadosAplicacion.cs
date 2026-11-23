using lib_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;
using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.Implementaciones
{
    public class TipoEmpleadosAplicacion : ITipoEmpleadosAplicacion
    {
        private IConexion IConexion;

        public TipoEmpleadosAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public TipoEmpleados Actualizar(TipoEmpleados entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (!Validar(entidad))
                throw new Exception("Falta informacion");

            var entry = this.IConexion!.Entry<TipoEmpleados>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public TipoEmpleados Borrar(TipoEmpleados entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            // No borrar si otras tablas lo usan (si no, SQL lanza error de llave foránea)
            if (this.IConexion.Empleados!.Any(x => x.TipoEmpleado == entidad.Id))
                throw new Exception("No se puede borrar el tipo de empleado porque tiene registros relacionados en: Empleados");

            this.IConexion.TipoEmpleados!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<TipoEmpleados> Buscar(TipoEmpleados entidad)
        {
            return this.IConexion.TipoEmpleados!

                .Where(x => x.Nombre!.Contains(entidad.Nombre!))

                

                .ToList();
        }

        public List<TipoEmpleados> Consultar()
        {
            return this.IConexion.TipoEmpleados!.ToList();
        }

        public TipoEmpleados Insertar(TipoEmpleados entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (!Validar(entidad))
                throw new Exception("Falta informacion");

            this.IConexion.TipoEmpleados!.Add(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        private bool Validar(TipoEmpleados entidad)
        {

            if (string.IsNullOrEmpty(entidad.Nombre) || entidad.SalarioBase <= 0)

                return false;
            return true;
        }
    }
}
