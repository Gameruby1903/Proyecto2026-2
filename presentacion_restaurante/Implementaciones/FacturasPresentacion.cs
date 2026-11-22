
using System.Collections.Generic;
using System.Text;
using presentacion_restaurante.Interfaces;
using lib_aplicaciones.Entidades;

namespace presentacion_restaurante.Implementaciones
{
    public class FacturasPresentacion : IFacturasPresentacion
    {

        private IComunicaciones IComunicaciones;

        public FacturasPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public Facturas Actualizar(Facturas entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (!Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Facturas>(
                "Facturas/Actualizar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public Facturas Borrar(Facturas entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Facturas>(
                "Facturas/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Facturas> Buscar(Facturas entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Facturas>>(
                "Facturas/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Facturas> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Facturas>>(
                "Facturas/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

        public Facturas Insertar(Facturas entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (!Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Facturas>(
                "Facturas/Insertar",
                entidad);
            task.Wait();
            return task.Result;
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

