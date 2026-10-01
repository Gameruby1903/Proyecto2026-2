using lib_aplicaciones.Entidades;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mst_Pruebas { 
    [TestClass]
    public class EmpleadosPruebas
    {
        private IConexion conexion;
        private Empleados? entidad;

        public EmpleadosPruebas()
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
            entidad = new Empleados()
            {
                Nombre = "santiago",
                Cedula = "35456",
                Telefono = "232131",
                FechaContratacion = DateTime.Now,
                TipoEmpleado = 1,
                Sucursal = 1

               
            };

            this.conexion.Empleados!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_empleados = this.conexion.Empleados!.ToList();
            if (lista_empleados.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Nombre = "samuel";

            this.conexion!.Empleados!.Update(this.entidad);
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Empleados!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}