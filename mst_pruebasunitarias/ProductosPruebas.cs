using lib_aplicaciones.Entidades;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mst_Pruebas
{

    [TestClass]
    public class ProductosPruebas
    {
        private IConexion conexion;
        private Productos? entidad;

        public ProductosPruebas()
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
            entidad = new Productos()
            {

                Nombre = "cata",
                Descripcion = "malo",
                Precio = 5645,
                Disponible = true,
                Categoria = 18
            };

            this.conexion.Productos!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_empleados = this.conexion.Productos!.ToList();
            if (lista_empleados.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Disponible = false;

            var entry = this.conexion!.Entry<Productos>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Productos!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
