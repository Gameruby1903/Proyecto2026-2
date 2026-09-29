using lib_aplicaciones.Entidades;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mst_Pruebas
{

    [TestClass]
    public class DetalleComprasPruebas
    {
        private IConexion conexion;
        private DetalleCompras? entidad;

        public DetalleComprasPruebas()
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
            entidad = new DetalleCompras()
            {
                Ingrediente = 1,
                Compra = 1,
                Cantidad = 4,
                PrecioUni = 12345
            };

            this.conexion.DetalleCompras!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_empleados = this.conexion.DetalleCompras!.ToList();
            if (lista_empleados.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Cantidad = 3;

            var entry = this.conexion!.Entry<DetalleCompras>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.DetalleCompras!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}