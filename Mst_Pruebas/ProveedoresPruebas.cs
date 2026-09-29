using lib_aplicaciones.Entidades;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mst_Pruebas
{

    [TestClass]
    public class ProveedoresPruebas
    {
        private IConexion conexion;
        private Proveedores? entidad;

        public ProveedoresPruebas()
        {
            this.conexion = new Conexion();
            conexion.StringConexion = "server=(localdb)\\MSSQLLocalDB;database=Restaurante3;Integrated Security=True" +
                ";TrustServerCertificate=true;";
        }

        [TestMethod]
        public void Ejecutar()
        {
            Insertar();
            Consultar();
            Actualizar();
            Borrar();
        }

        private void Insertar()
        {
            entidad = new Proveedores()
            {

                Nombre = "cata",
                Telefono = "2616",
                Email = "ecc@55",
                Direccion = "castilla"
                
            };

            this.conexion.Proveedores!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_empleados = this.conexion.Proveedores!.ToList();
            if (lista_empleados.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Nombre = "lina";

            var entry = this.conexion!.Entry<Proveedores>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Proveedores!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}