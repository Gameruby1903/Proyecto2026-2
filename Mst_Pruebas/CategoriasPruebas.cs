using lib_aplicaciones.Entidades;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mst_Pruebas { 

    [TestClass]
    public class CategoriasPruebas
    {
        private IConexion conexion;
        private Categorias? entidad;

        public CategoriasPruebas()
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
            entidad = new Categorias()
            {

                Nombre = "Prueba",
                Descripcion = "Bien",
                Disponible = true,
                OrdenMenu = 1
            };

            this.conexion.Categorias!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_empleados = this.conexion.Categorias!.ToList();
            if (lista_empleados.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Disponible = false;

            var entry = this.conexion!.Entry<Categorias>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Categorias!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
