using lib_aplicaciones.Entidades;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mst_Pruebas
{

    [TestClass]
    public class ResenasPruebas
    {
        private IConexion conexion;
        private Resenas? entidad;

        public ResenasPruebas()
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
            entidad = new Resenas()
            {

                Cliente = 11,
                Producto = 10,
                Calificacion = 4,
                Comentario = "buenas",
                Fecha = DateTime.Now
            };

            this.conexion.Resenas!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_empleados = this.conexion.Resenas!.ToList();
            if (lista_empleados.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Comentario = "malo";

            var entry = this.conexion!.Entry<Resenas>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Resenas!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
