using lib_aplicaciones.Entidades;

namespace presentacion_restaurante.Interfaces
{
    public interface IUsuariosPresentacion
    {
        Usuarios Insertar(Usuarios entidad);
        Usuarios Actualizar(Usuarios entidad);
        Usuarios Borrar(Usuarios entidad);

        List<Usuarios> Consultar();
        List<Usuarios> Buscar(Usuarios entidad);

        Usuarios? IniciarSesion(Usuarios entidad);
        List<string> ConsultarPermisos(Usuarios entidad);

        List<Usuarios> ConsultarPorRol(Usuarios entidad);
    }
}