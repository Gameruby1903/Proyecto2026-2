using lib_aplicaciones.Entidades;

namespace presentacion_restaurante.Interfaces
{
    public interface IPermisosPresentacion
    {
        Permisos Insertar(Permisos entidad);
        Permisos Actualizar(Permisos entidad);
        Permisos Borrar(Permisos entidad);

        List<Permisos> Consultar();
        List<Permisos> Buscar(Permisos entidad);
    }
}