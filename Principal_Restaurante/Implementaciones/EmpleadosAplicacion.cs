using lib_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;
using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.Implementaciones
{
    public class EmpleadosAplicacion : IEmpleadosAplicacion
    {
        private IConexion IConexion;

        public EmpleadosAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Empleados Actualizar(Empleados entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var entry = this.IConexion!.Entry<Empleados>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public Empleados Borrar(Empleados entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.Empleados!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Empleados> Buscar(Empleados entidad)
        {
            return this.IConexion.Empleados!.Include(x => x._TipoEmpleado).Include(x => x._Sucursal)
                .Where(x => x.Nombre!.Contains(entidad.Nombre!))
                .ToList();
        }

        public List<Empleados> Consultar()
        {
            return this.IConexion.Empleados!.Include(x => x._TipoEmpleado).Include(x => x._Sucursal).ToList();
        }

        public Empleados Insertar(Empleados entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            this.IConexion.Empleados!.Add(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        private bool Validar(Empleados entidad)
        {
            return string.IsNullOrEmpty(entidad.Nombre) || (string.IsNullOrEmpty(entidad.Cedula) ||
                string.IsNullOrEmpty(entidad.Telefono) || entidad.FechaContratacion == DateTime.MinValue ||
                entidad.TipoEmpleado == 0 || entidad.Sucursal == 0);
                
        }
    }
}
