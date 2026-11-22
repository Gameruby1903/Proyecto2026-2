
    using lib_aplicaciones.Entidades;

    namespace lib_aplicaciones.Interfaces
    {
        public interface IRolesAplicacion
        {
            Roles Insertar(Roles entidad);
            Roles Actualizar(Roles entidad);
            Roles Borrar(Roles entidad);

            List<Roles> Consultar();
            List<Roles> Buscar(Roles entidad);
        }
    }

