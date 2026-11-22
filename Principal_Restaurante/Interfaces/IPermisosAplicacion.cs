using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IPermisosAplicacion
    {
        Permisos Insertar(Permisos entidad);
        Permisos Actualizar(Permisos entidad);
        Permisos Borrar(Permisos entidad);

        List<Permisos> Consultar();
        List<Permisos> Buscar(Permisos entidad);
    }
}
