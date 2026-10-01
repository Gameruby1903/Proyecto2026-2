using lib_aplicaciones.Entidades;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mst_Pruebas
{

    [TestClass]
    public class InventariosPruebas
    {
        private IConexion conexion;
        private Inventarios? entidad;

        public InventariosPruebas()
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
            entidad = new Inventarios()
            {

                Ingrediente = 1,
                CantDisponible = 12,
                CantMinima = 10,
                UltimaActu = DateTime.Now          
            };

            this.conexion.Inventarios!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_empleados = this.conexion.Inventarios!.ToList();
            if (lista_empleados.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.CantMinima = 9;

            var entry = this.conexion!.Entry<Inventarios>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Inventarios!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}