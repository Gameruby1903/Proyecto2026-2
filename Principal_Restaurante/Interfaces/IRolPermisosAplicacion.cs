using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IRolPermisosAplicacion
    {
        RolPermisos Insertar(RolPermisos entidad);
        RolPermisos Actualizar(RolPermisos entidad);
        RolPermisos Borrar(RolPermisos entidad);

        List<RolPermisos> Consultar();
        List<RolPermisos> Buscar(RolPermisos entidad);

        List<RolPermisos> ConsultarPorRol(RolPermisos entidad);
    }
}