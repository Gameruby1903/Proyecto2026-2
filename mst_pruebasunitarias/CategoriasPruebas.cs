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

            conexion.StringConexion = "server=DESKTOP-1EHV8G7\\DEV;database=Restaurante;Integrated Security=True;TrustServerCertificate=true;";


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

            

            this.conexion!.Categorias!.Update(this.entidad);
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Categorias!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
