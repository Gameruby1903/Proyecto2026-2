using lib_aplicaciones.Entidades;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mst_Pruebas
{

    [TestClass]
    public class RecetaIngredientesPruebas
    {
        private IConexion conexion;
        private RecetaIngredientes? entidad;

        public RecetaIngredientesPruebas()
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
            entidad = new RecetaIngredientes()
            {

                Producto = 1,
                Ingrediente = 1,
                CantiReque = 45,
                Notas = "mucho"
            };

            this.conexion.RecetaIngredientes!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_empleados = this.conexion.RecetaIngredientes!.ToList();
            if (lista_empleados.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.CantiReque = 40;

            var entry = this.conexion!.Entry<RecetaIngredientes>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.RecetaIngredientes!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}