using lib_aplicaciones.Entidades;
using presentacion_restaurante.Interfaces;

namespace presentacion_restaurante.Implementaciones
{
    public class UsuariosPresentacion : IUsuariosPresentacion
    {
        private IComunicaciones IComunicaciones;

        public UsuariosPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public Usuarios Insertar(Usuarios entidad)
        {
            var task = this.IComunicaciones.Ejecutar<Usuarios>(
                "Usuarios/Insertar", entidad);
            task.Wait();
            return task.Result;
        }

        public Usuarios Actualizar(Usuarios entidad)
        {
            var task = this.IComunicaciones.Ejecutar<Usuarios>(
                "Usuarios/Actualizar", entidad);
            task.Wait();
            return task.Result;
        }

        public Usuarios Borrar(Usuarios entidad)
        {
            var task = this.IComunicaciones.Ejecutar<Usuarios>(
                "Usuarios/Borrar", entidad);
            task.Wait();
            return task.Result;
        }

        public List<Usuarios> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Usuarios>>(
                "Usuarios/Consultar", null);
            task.Wait();
            return task.Result;
        }

        public List<Usuarios> Buscar(Usuarios entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Usuarios>>(
                "Usuarios/Buscar", entidad);
            task.Wait();
            return task.Result;
        }

        public Usuarios? IniciarSesion(Usuarios entidad)
        {
            var task = this.IComunicaciones.Ejecutar<Usuarios?>(
                "Usuarios/IniciarSesion", entidad);
            task.Wait();
            return task.Result;
        }

        public List<Usuarios> ConsultarPorRol(Usuarios entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Usuarios>>(
                "Usuarios/ConsultarPorRol", entidad);
            task.Wait();
            return task.Result;
        }
        public List<string> ConsultarPermisos(Usuarios entidad)
        {
            var task = this.IComunicaciones
                .Ejecutar<List<string>>("Usuarios/ConsultarPermisos", entidad);

            task.Wait();

            return task.Result;
        }
    }
}