
using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IUsuariosAplicacion
    {
        Usuarios Insertar(Usuarios entidad);
        Usuarios Actualizar(Usuarios entidad);
        Usuarios Borrar(Usuarios entidad);

        List<Usuarios> Consultar();
        List<Usuarios> Buscar(Usuarios entidad);

        Usuarios? IniciarSesion(Usuarios entidad);
        List<Usuarios> ConsultarPorRol(Usuarios entidad);
        List<string> ConsultarPermisos(Usuarios entidad);
        List<string> ConsultarPermisosPorNombre(string nombre);
    }
}
