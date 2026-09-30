using lib_aplicaciones.Entidades;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;



namespace Mst_Pruebas
{

    [TestClass]
    public class PedidosPruebas
    {
        private IConexion conexion;
        private Pedidos? entidad;

        public PedidosPruebas()
        {
            this.conexion = new Conexion();
            conexion.StringConexion = "server=localhost;database=Restaurante;Integrated Security=True" +
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
            entidad = new Pedidos()
            {
                Fecha = DateTime.Now,
                Cliente = 8,
                Mesa = 11,
                Empleado = 23,
                Estado = "entregado"
            };

            this.conexion.Pedidos!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_empleados = this.conexion.Pedidos!.ToList();
            if (lista_empleados.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Estado = "cocinando";

            var entry = this.conexion!.Entry<Pedidos>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Pedidos!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
