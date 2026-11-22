using lib_aplicaciones.Entidades;
using presentacion_restaurante.Interfaces;

namespace presentacion_restaurante.Implementaciones
{
    public class PermisosPresentacion : IPermisosPresentacion
    {
        private IComunicaciones IComunicaciones;

        public PermisosPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public Permisos Insertar(Permisos entidad)
        {
            var task = this.IComunicaciones.Ejecutar<Permisos>(
                "Permisos/Insertar", entidad);
            task.Wait();
            return task.Result;
        }

        public Permisos Actualizar(Permisos entidad)
        {
            var task = this.IComunicaciones.Ejecutar<Permisos>(
                "Permisos/Actualizar", entidad);
            task.Wait();
            return task.Result;
        }

        public Permisos Borrar(Permisos entidad)
        {
            var task = this.IComunicaciones.Ejecutar<Permisos>(
                "Permisos/Borrar", entidad);
            task.Wait();
            return task.Result;
        }

        public List<Permisos> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Permisos>>(
                "Permisos/Consultar", null);
            task.Wait();
            return task.Result;
        }

        public List<Permisos> Buscar(Permisos entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Permisos>>(
                "Permisos/Buscar", entidad);
            task.Wait();
            return task.Result;
        }
    }
}