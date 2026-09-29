using lib_aplicaciones.Entidades;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;



namespace Mst_Pruebas
{

    [TestClass]
    public class MetodoPagosPruebas
    {
        private IConexion conexion;
        private MetodoPagos? entidad;

        public MetodoPagosPruebas()
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
            entidad = new MetodoPagos()
            {

              Tipo = "efectivo",
              Descripcion = "bien"
            };

            this.conexion.MetodoPagos!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_empleados = this.conexion.MetodoPagos!.ToList();
            if (lista_empleados.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Tipo = "transferencia";

            var entry = this.conexion!.Entry<MetodoPagos>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.MetodoPagos!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}