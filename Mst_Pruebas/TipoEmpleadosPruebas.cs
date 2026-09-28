using lib_aplicaciones.Entidades;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mst_Pruebas { 
    [TestClass]
    public class TipoEmpleadosPruebas
    {
        private IConexion conexion;
        private TipoEmpleados? entidad;

        public TipoEmpleadosPruebas()
        {
            this.conexion = new Conexion();
            conexion.StringConexion = "server=(localdb)\\MSSQLLocalDB;database=Restaurante;Integrated Security=True" +
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
            entidad = new TipoEmpleados()
            {
                nombre = "mesero",
                SalarioBase = 111
                
            };

            this.conexion.TipoEmpleados!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_empleados = this.conexion.TipoEmpleados!.ToList();
            if (lista_empleados.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.nombre = "mesero";

            var entry = this.conexion!.Entry<TipoEmpleados>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.TipoEmpleados!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}