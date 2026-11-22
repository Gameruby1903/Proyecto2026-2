using lib_aplicaciones.Entidades;
using presentacion_restaurante.Interfaces;

namespace presentacion_restaurante.Implementaciones
{
    public class RolesPresentacion : IRolesPresentacion
    {
        private IComunicaciones IComunicaciones;

        public RolesPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public Roles Insertar(Roles entidad)
        {
            var task = this.IComunicaciones.Ejecutar<Roles>(
                "Roles/Insertar", entidad);
            task.Wait();
            return task.Result;
        }

        public Roles Actualizar(Roles entidad)
        {
            var task = this.IComunicaciones.Ejecutar<Roles>(
                "Roles/Actualizar", entidad);
            task.Wait();
            return task.Result;
        }

        public Roles Borrar(Roles entidad)
        {
            var task = this.IComunicaciones.Ejecutar<Roles>(
                "Roles/Borrar", entidad);
            task.Wait();
            return task.Result;
        }

        public List<Roles> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Roles>>(
                "Roles/Consultar", null);
            task.Wait();
            return task.Result;
        }

        public List<Roles> Buscar(Roles entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Roles>>(
                "Roles/Buscar", entidad);
            task.Wait();
            return task.Result;
        }
    }
}