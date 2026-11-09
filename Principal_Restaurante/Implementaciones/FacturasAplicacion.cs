
using lib_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;
using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.Implementaciones
{
    public class FacturasAplicacion : IFacturasAplicacion
    {
        private IConexion IConexion;

        public FacturasAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

     
        public Facturas Actualizar(Facturas entidad)
        {

            if (entidad.Id == 0)
                throw new Exception("No se ha guardado");

        
            if (!Validar(entidad))
                throw new Exception("Falta informacion");

            
            var entry = this.IConexion!.Entry<Facturas>(entidad);
            entry.State = EntityState.Modified;

            
            this.IConexion!.SaveChanges();

            return entidad;
        }

      
        public Facturas Borrar(Facturas entidad)
        {
     
            if (entidad.Id == 0)
                throw new Exception("No se ha guardado");

         
            this.IConexion.Facturas!.Remove(entidad!);

          
            this.IConexion.SaveChanges();

            return entidad;
        }

       
        public List<Facturas> Buscar(Facturas entidad)
        {
            // Si no se proporciona una fecha,
            // devuelve todas las facturas.
            if (entidad.FechaEmi == DateTime.MinValue)
                return this.IConexion.Facturas!.ToList();

            // Si se proporciona una fecha,
            // busca las facturas que correspondan al mismo día.
            return this.IConexion.Facturas!
                .Where(x => x.FechaEmi.Date == entidad.FechaEmi.Date)
                .ToList();
        }

        
        public List<Facturas> Consultar()
        {
            return this.IConexion.Facturas!.ToList();
        }

   
        public Facturas Insertar(Facturas entidad)
        {
   
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (!Validar(entidad))
                throw new Exception("Falta informacion");

        
            this.IConexion.Facturas!.Add(entidad!);

           
            this.IConexion.SaveChanges();

            return entidad;
        }

        private bool Validar(Facturas entidad)
        {
            // La fecha no puede estar vacía o tener el valor mínimo.
            if (entidad.FechaEmi == DateTime.MinValue)
                return false;

            // Si cumple la validación, la información es correcta.
            return true;
        }
    }
}
