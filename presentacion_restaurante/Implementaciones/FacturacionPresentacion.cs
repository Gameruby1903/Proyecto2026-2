using lib_aplicaciones.Entidades;
using presentacion_restaurante.Interfaces;

namespace presentacion_restaurante.Implementaciones
{
    public class FacturacionPresentacion : IFacturacionPresentacion
    {
        private IComunicaciones IComunicaciones;

        public FacturacionPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public List<Pedidos> PedidosPendientes()
        {
            var task = this.IComunicaciones.Ejecutar<List<Pedidos>>(
                "Facturacion/PedidosPendientes",
                null!);
            task.Wait();
            return task.Result;
        }

        public Facturas Calcular(Facturas entidad)
        {
            if (entidad.Pedido <= 0)
                throw new Exception("Seleccione un pedido");

            var task = this.IComunicaciones.Ejecutar<Facturas>(
                "Facturacion/Calcular",
                entidad);
            task.Wait();
            return task.Result;
        }

        public Facturas Facturar(Facturas entidad)
        {
            if (entidad.Pedido <= 0)
                throw new Exception("Seleccione un pedido");

            if (entidad.MetodoPago <= 0)
                throw new Exception("Seleccione un metodo de pago");

            var task = this.IComunicaciones.Ejecutar<Facturas>(
                "Facturacion/Facturar",
                entidad);
            task.Wait();
            return task.Result;
        }
    }
}
