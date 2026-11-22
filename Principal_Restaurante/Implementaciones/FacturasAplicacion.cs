
using lib_aplicaciones.Entidades;
using lib_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

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
                return this.IConexion.Facturas!.Include(x => x._Pedido).Include(x => x._MetodoPago).ToList();

            // Si se proporciona una fecha,
            // busca las facturas que correspondan al mismo día.
            return this.IConexion.Facturas!.Include(x => x._Pedido).Include(x => x._MetodoPago)
                .Where(x => x.FechaEmi.Date == entidad.FechaEmi.Date)
                .ToList();
        }


        public List<Facturas> Consultar()
        {
            return this.IConexion.Facturas!.Include(x => x._Pedido).Include(x => x._MetodoPago).ToList();
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
            if (entidad.FechaEmi == DateTime.MinValue || entidad.Pedido == 0 || 
                entidad.MetodoPago == 0 || entidad.Total == 0 || entidad.Impuesto == 0 || entidad.Subtotal == 0 
                || entidad.FechaEmi == DateTime.MinValue)
                return false;

            // Si cumple la validación, la información es correcta.
            return true;
        }
    }



}

