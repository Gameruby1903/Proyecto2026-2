using lib_aplicaciones.Entidades;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;



namespace Mst_Pruebas
{

    [TestClass]
    public class DetallePedidosPruebas
    {
        private IConexion conexion;
        private DetallePedidos? entidad;

        public DetallePedidosPruebas()
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
            entidad = new DetallePedidos()
            {

                Pedido = 12,
                Producto = 10,
                Cantidad = 1,
                PrecioUni = 45454
            };

            this.conexion.DetallePedidos!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_empleados = this.conexion.DetallePedidos!.ToList();
            if (lista_empleados.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.PrecioUni = 66;

            var entry = this.conexion!.Entry<DetallePedidos>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.DetallePedidos!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
