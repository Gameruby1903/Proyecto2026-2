using lib_aplicaciones.Entidades;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Mst_Pruebas
{

    [TestClass]
    public class MesasPruebas
    {
        private IConexion conexion;
        private Mesas? entidad;

        public MesasPruebas()
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
            entidad = new Mesas()
            {

                Numero = 1,
                Capacidad = 1,
                Estado = true,
                Ubicacion = "avenida"
            };

            this.conexion.Mesas!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_empleados = this.conexion.Mesas!.ToList();
            if (lista_empleados.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Estado = false;

            var entry = this.conexion!.Entry<Mesas>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Mesas!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}