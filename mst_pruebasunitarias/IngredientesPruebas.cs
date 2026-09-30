using lib_aplicaciones.Entidades;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mst_Pruebas
{

    [TestClass]
    public class IngredientesPruebas
    {
        private IConexion conexion;
        private Ingredientes? entidad;

        public IngredientesPruebas()
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
            entidad = new Ingredientes()
            {

                Nombre = "arroz",
                UniMedida = "1onz",
                CostoUni = 6554,
                Proveedor = 10
            };

            this.conexion.Ingredientes!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_empleados = this.conexion.Ingredientes!.ToList();
            if (lista_empleados.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Nombre = "spagetti";

            var entry = this.conexion!.Entry<Ingredientes>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Ingredientes!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}