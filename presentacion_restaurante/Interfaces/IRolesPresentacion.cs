using lib_aplicaciones.Entidades;

namespace presentacion_restaurante.Interfaces
{
    public interface IRolesPresentacion
    {
        Roles Insertar(Roles entidad);
        Roles Actualizar(Roles entidad);
        Roles Borrar(Roles entidad);

        List<Roles> Consultar();
        List<Roles> Buscar(Roles entidad);
    }
}