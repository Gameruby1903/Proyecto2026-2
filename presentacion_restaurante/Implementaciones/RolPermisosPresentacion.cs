using lib_aplicaciones.Entidades;
using presentacion_restaurante.Interfaces;

namespace presentacion_restaurante.Implementaciones
{
    public class RolPermisosPresentacion : IRolPermisosPresentacion
    {
        private IComunicaciones IComunicaciones;

        public RolPermisosPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public RolPermisos Insertar(RolPermisos entidad)
        {
            var task = this.IComunicaciones.Ejecutar<RolPermisos>(
                "RolPermisos/Insertar", entidad);
            task.Wait();
            return task.Result;
        }

        public RolPermisos Actualizar(RolPermisos entidad)
        {
            var task = this.IComunicaciones.Ejecutar<RolPermisos>(
                "RolPermisos/Actualizar", entidad);
            task.Wait();
            return task.Result;
        }

        public RolPermisos Borrar(RolPermisos entidad)
        {
            var task = this.IComunicaciones.Ejecutar<RolPermisos>(
                "RolPermisos/Borrar", entidad);
            task.Wait();
            return task.Result;
        }

        public List<RolPermisos> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<RolPermisos>>(
                "RolPermisos/Consultar", null);
            task.Wait();
            return task.Result;
        }

        public List<RolPermisos> Buscar(RolPermisos entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<RolPermisos>>(
                "RolPermisos/Buscar", entidad);
            task.Wait();
            return task.Result;
        }

        public List<RolPermisos> ConsultarPorRol(RolPermisos entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<RolPermisos>>(
                "RolPermisos/ConsultarPorRol", entidad);
            task.Wait();
            return task.Result;
        }
    }
}