using lib_aplicaciones.Entidades;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mst_Pruebas
{

    [TestClass]
    public class ReservasPruebas
    {
        private IConexion conexion;
        private Reservas? entidad;

        public ReservasPruebas()
        {
            this.conexion = new Conexion();
            conexion.StringConexion = "server=DESKTOP-1EHV8G7\\DEV;database=Restaurante;Integrated Security=True" +
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
            entidad = new Reservas()
            {

                Cliente = 1,
                Mesa = 1,
                FechaHora = DateTime.Now,
                NPersonas = 4,
                Estado = "disponible"
            };

            this.conexion.Reservas!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_empleados = this.conexion.Reservas!.ToList();
            if (lista_empleados.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Estado = "ocupada";

            var entry = this.conexion!.Entry<Reservas>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Reservas!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}