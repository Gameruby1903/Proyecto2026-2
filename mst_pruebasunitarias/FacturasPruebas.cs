using lib_aplicaciones.Entidades;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Mst_Pruebas
{

    [TestClass]
    public class FacturasPruebas
    {
        private IConexion conexion;
        private Facturas? entidad;

        public FacturasPruebas()
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
            entidad = new Facturas()
            {

                Pedido = 12,
                FechaEmi = DateTime.Now,
                Subtotal = 564,
                Impuesto = 5574454,
                Total = 4455,
                MetodoPago = 9
            };

            this.conexion.Facturas!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_empleados = this.conexion.Facturas!.ToList();
            if (lista_empleados.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Total = 0;

            var entry = this.conexion!.Entry<Facturas>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Facturas!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}