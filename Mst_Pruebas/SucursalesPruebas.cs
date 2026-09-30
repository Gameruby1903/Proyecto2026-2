using lib_aplicaciones.Entidades;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mst_Pruebas { 
    [TestClass]
    public class SucursalesPruebas
    {
        private IConexion conexion;
        private Sucursales? entidad;

        public SucursalesPruebas()
        {
            this.conexion = new Conexion();

            conexion.StringConexion = "server=(localdb)\\MSSQLLocalDB;database=Restaurante3;Integrated Security=True;"  +
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
            entidad = new Sucursales()
            {
                Nombre = "mesero",
                Direccion = "ave 4",
                Telefono = "151616166" ,
                Ciudad = "Medellin"

                
            };

            this.conexion.Sucursales!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_empleados = this.conexion.Sucursales!.ToList();
            if (lista_empleados.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Ciudad = " cali";

            

            this.conexion!.Sucursales!.Update(this.entidad);
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Sucursales!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}