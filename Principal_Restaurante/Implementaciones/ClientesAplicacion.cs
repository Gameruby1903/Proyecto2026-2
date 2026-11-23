using lib_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;
using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.Implementaciones
{
    public class ClientesAplicacion : IClientesAplicacion
    {
        private IConexion IConexion;

        public ClientesAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Clientes Actualizar(Clientes entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var entry = this.IConexion!.Entry<Clientes>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public Clientes Borrar(Clientes entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            // No borrar si otras tablas lo usan (si no, SQL lanza error de llave foránea)
            if (this.IConexion.Resenas!.Any(x => x.Cliente == entidad.Id) ||
                this.IConexion.Pedidos!.Any(x => x.Cliente == entidad.Id) ||
                this.IConexion.Reservas!.Any(x => x.Cliente == entidad.Id) ||
                this.IConexion.Usuarios!.Any(x => x.Cliente == entidad.Id))
                throw new Exception("No se puede borrar el cliente porque tiene registros relacionados en: Resenas, Pedidos, Reservas, Usuarios");

            this.IConexion.Clientes!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Clientes> Buscar(Clientes entidad)
        {
            return this.IConexion.Clientes!
                .Where(x => x.Cedula!.Contains(entidad.Cedula!))
                .ToList();
        }

        public List<Clientes> Consultar()
        {
            return this.IConexion.Clientes!.ToList();
        }

        public Clientes Insertar(Clientes entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            this.IConexion.Clientes!.Add(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        private bool Validar(Clientes entidad)
        {
            return string.IsNullOrEmpty(entidad.Nombre) ||
                string.IsNullOrEmpty(entidad.Cedula) ||
                string.IsNullOrEmpty(entidad.Telefono) ||
                string.IsNullOrEmpty(entidad.Email);
        }
    }
}
